using System;

namespace WebSocket.Server
{
    [Serializable]
    public class Message
    {
        public string User { get; set; }
        public string Content { get; set; }
        public string UserType { get; set; }
        public string LocalTime { get; set; }

        public Message() 
        {
            UserType = "user";
        }
    }
}