// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: acd.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Acd {
  public static partial class DialogScribe
  {
    static readonly string __ServiceName = "acd.DialogScribe";

    static readonly grpc::Marshaller<global::Acd.ProcessRequest> __Marshaller_acd_ProcessRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Acd.ProcessRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Acd.ProcessResponse> __Marshaller_acd_ProcessResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Acd.ProcessResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::Acd.ProcessRequest, global::Acd.ProcessResponse> __Method_Process = new grpc::Method<global::Acd.ProcessRequest, global::Acd.ProcessResponse>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "Process",
        __Marshaller_acd_ProcessRequest,
        __Marshaller_acd_ProcessResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Acd.AcdReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for DialogScribe</summary>
    public partial class DialogScribeClient : grpc::ClientBase<DialogScribeClient>
    {
      /// <summary>Creates a new client for DialogScribe</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public DialogScribeClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for DialogScribe that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public DialogScribeClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected DialogScribeClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected DialogScribeClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual grpc::AsyncDuplexStreamingCall<global::Acd.ProcessRequest, global::Acd.ProcessResponse> Process(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Process(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncDuplexStreamingCall<global::Acd.ProcessRequest, global::Acd.ProcessResponse> Process(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_Process, null, options);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override DialogScribeClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new DialogScribeClient(configuration);
      }
    }

  }
}
#endregion