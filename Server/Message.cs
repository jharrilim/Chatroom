using System;

namespace WebSocket.Server
{
    [Serializable]
    public class Message
    {
        public string User { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public double UnixTime { get; set; }

        public Message() 
        {
            Type = "user";
        }
    }
}