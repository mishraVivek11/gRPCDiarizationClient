using GRPCClientLib.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GRPCClientLib
{
    public class ProcessHandler : IProcessHandler
    {
        private BlockingCollection<ProcessResponseModel> processModelQueue = new BlockingCollection<ProcessResponseModel>();
        private Dictionary<int, TranscriptTurns> TranscriptMap = new Dictionary<int, TranscriptTurns>();

        public event EventHandler<TranscriptDataEventArgs> TranscriptDataAvailable;


        public ProcessHandler()
        {
            var threadTranscript = new Thread(ProcessQueueItems)
            { IsBackground = true };
            threadTranscript.Start();
        }

        public void ProcessResponse(ProcessResponseModel responseModel)
        {
            if (responseModel != null && !processModelQueue.IsAddingCompleted)
            {
                processModelQueue.Add(responseModel, CancellationToken.None);
            }
        }

        private void ProcessQueueItems()
        {
            while (!processModelQueue.IsCompleted)
            {
                ProcessResponseModel currentresponseModel = processModelQueue.Take();
                if (currentresponseModel.transcript != null)
                {
                    foreach (var turn in currentresponseModel.transcript.turns)
                    {
                        if (!TranscriptMap.ContainsKey(turn.id))
                        {
                            TranscriptMap.Add(turn.id, turn);
                            TranscriptDataAvailable(this, new TranscriptDataEventArgs(TranscriptMap.Values.ToList()));
                        }
                        else
                        {
                            if (!TranscriptMap[turn.id].stable)
                            {
                                string textValueBeforeUpdate = TranscriptMap[turn.id].text;
                                TranscriptMap[turn.id] = turn;
                                if (string.IsNullOrEmpty(TranscriptMap[turn.id].text))
                                { TranscriptMap[turn.id].text = textValueBeforeUpdate; }
                                TranscriptDataAvailable(this, new TranscriptDataEventArgs(TranscriptMap.Values.ToList()));
                            }
                        }
                    }
                }
                else if (currentresponseModel.preview != null)
                {
                    if (!TranscriptMap[currentresponseModel.preview.turn.id].stable)
                    {
                        TranscriptMap[currentresponseModel.preview.turn.id].text = currentresponseModel.preview.text;
                        TranscriptDataAvailable(this, new TranscriptDataEventArgs(TranscriptMap.Values.ToList()));
                    }
                }
            }
        }
    }
    public class TranscriptDataEventArgs : EventArgs
    {
        public IReadOnlyCollection<TranscriptTurns> Transcripts { get; private set; }

        public TranscriptDataEventArgs(IReadOnlyCollection<TranscriptTurns> transcripts)
        {
            Transcripts = transcripts;
        }
    }
}
