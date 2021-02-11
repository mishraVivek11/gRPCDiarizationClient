using System;
using System.Collections.Generic;
using System.Text;

namespace GRPCClientLib.Models
{
    public class Message
    {
        public string type { get; set; }
        public int version { get; set; }
    }

    public class Stream
    {
        public string name { get; set; }
        public string type { get; set; }
        public int rate { get; set; }
        public int channels { get; set; }
    }


    public class Speaker
    {
        public string id { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public int stream { get; set; }
        public int channel { get; set; }
    }

    public class Init
    {
        public Stream[] streams { get; set; }
        public Speaker[] speakers { get; set; }
    }

    public class InitRequest
    {
        public Message message { get; set; }
        public Init init { get; set; }
    }

    public class Framegen
    {
        public int frame { get; set; }
        public long time { get; set; }
    }

    public class Info
    {
        public Framegen[] framegen { get; set; }
    }

    public class TranscriptTurns
    {
        public int id { get; set; }
        public int revision { get; set; }
        public bool stable { get; set; }
        public string speaker { get; set; }
        public long start { get; set; }
        public long end { get; set; }
        public string text { get; set; }
    }

    public class Transcript
    {
        public int revision { get; set; }
        public TranscriptTurns[] turns { get; set; }
    }

    public class PreviewTurn
    {
        public int id { get; set; }
        public int revision { get; set; }
    }

    public class Preview
    {
        public PreviewTurn turn { get; set; }
        public string speaker { get; set; }
        public long start { get; set; }
        public long end { get; set; }
        public string text { get; set; }
    }

    public class ProcessResponseModel
    {
        public Message message { get; set; }
        public Transcript transcript { get; set; }
        public Preview preview { get; set; }
    }
}
