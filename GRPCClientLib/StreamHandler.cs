using Acd;
using Grpc.Core;
using Grpc.Net.Client;
using GRPCClientLib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

namespace GRPCClientLib
{
    public class StreamHandler : IStreamHandler
    {
        private readonly GrpcChannel channel;
        private readonly DialogScribe.DialogScribeClient client;
        private IProcessHandler processhandler = null;

        public StreamHandler(IProcessHandler processhndlr)
        {
            processhandler = processhndlr;
            //channel = GrpcChannel.ForAddress("https://localhost:5001");
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            Uri uri = new Uri("http://wien-hiring.eastus2.cloudapp.azure.com:80");
            channel = GrpcChannel.ForAddress(uri);
            client = new Acd.DialogScribe.DialogScribeClient(channel);
        }

        public async Task InitStreamingAsync(string initModel)
        {
            using (var call = client.Process())
            {
                var responseReaderTask = Task.Run(async () =>
                {
                    while (await call.ResponseStream.MoveNext())
                    {
                        var processResponse = call.ResponseStream.Current;

                        var responseModel = JsonConvert.DeserializeObject<ProcessResponseModel>(processResponse.Body,
                            new JsonSerializerSettings
                            {
                                Error = delegate (object sender, ErrorEventArgs args)
                                {
                                    args.ErrorContext.Handled = true;
                                }
                            });

                        processhandler.ProcessResponse(responseModel);
                    }
                });

                await call.RequestStream.WriteAsync(new Acd.ProcessRequest() { Init = new Acd.InitRequest() { Body = initModel } });

                var audioStreamRequestTask = Task.Run(async () =>
                {
                    while (true) // a breaking condition to notify end of audio transmission
                    {
                        //var data = new DataChunk();
                        //await call.RequestStream.WriteAsync(new Acd.ProcessRequest() { Data = new DataChunk() });
                    }
                    await call.RequestStream.CompleteAsync();
                });

                await Task.WhenAll(responseReaderTask, audioStreamRequestTask);
            }
        }
    }
}
