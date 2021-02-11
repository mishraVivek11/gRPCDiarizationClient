using GRPCClientLib.Models;
using System;

namespace GRPCClientLib
{
    public interface IProcessHandler
    {
        event EventHandler<TranscriptDataEventArgs> TranscriptDataAvailable;

        void ProcessResponse(ProcessResponseModel responseModel);
    }
}