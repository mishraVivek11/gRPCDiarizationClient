using GRPCClientLib;
using GRPCClientLib.Models;
using System;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace DiarizationClient
{
    public partial class Form1 : Form
    {
        IStreamHandler streamHandler = null;
        IProcessHandler processHandler = null;

        public Form1()
        {
            InitializeComponent();
            processHandler = new ProcessHandler();
            streamHandler = new StreamHandler(processHandler);
            processHandler.TranscriptDataAvailable += ProcessHandler_TranscriptDataAvailable;
        }

        private void ProcessHandler_TranscriptDataAvailable(object sender, TranscriptDataEventArgs e)
        {
            richTextBox1.Invoke((Action)(() =>
            {
                richTextBox1.Clear();
                StringBuilder stableText = new StringBuilder();
                StringBuilder previewText = new StringBuilder();
                foreach (var turn in e.Transcripts)
                {
                    if (!string.IsNullOrEmpty(turn.text))
                    {
                        if (turn.stable)
                        {
                            stableText.Append($"{turn.speaker}: {turn.text}{Environment.NewLine}");
                        }
                        else
                        {
                            previewText.Append($"{turn.speaker}: {turn.text}{Environment.NewLine}");
                        }
                    }
                }

                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.SelectionColor = Color.Black;
                richTextBox1.AppendText(stableText.ToString());
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.SelectionColor = Color.DarkGray;
                richTextBox1.AppendText(previewText.ToString());

            }));
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await streamHandler.InitStreamingAsync(GetInitModel());
        }

        private string GetInitModel()
        {
            var initReq = new InitRequest()
            {
                message = new GRPCClientLib.Models.Message() { type = "init", version = 1 },
                init = new Init()
                {
                    streams = new Stream[] { new Stream() { name = "lapel mic", type = "audio/L16", rate = 16000, channels = 2 } },
                    speakers = new Speaker[]
                    {
                        new Speaker(){ id="1000", name="Dr. Harris", role="doctor",stream=0,channel=0},
                        new Speaker(){id="1001", name="Vivek", role="patient",stream=0,channel=1}
                    }
                }
            };
            return JsonSerializer.Serialize(initReq);
        }
    }
}
