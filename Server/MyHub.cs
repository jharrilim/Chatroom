using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Security;
using System.Threading.Tasks;

namespace WebSocket.Server
{
    public class MyHub : Hub
    {
        private static readonly int messageLimit = 100;
        private static readonly ConcurrentQueue<Message> messages;

        static MyHub()
        {
            messages = new ConcurrentQueue<Message>();
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("GetChatHistory", messages.ToList());
            await Clients.Caller.SendAsync("SelfJoined", Context.ConnectionId);
            await Clients.Others.SendAsync("UserJoined", Context.ConnectionId);
            await SendMessage("System", $"{Context.ConnectionId} has joined the room.", "system");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            
            await SendMessage("System", $"{Context.ConnectionId} has left the room.", "system");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            await SendMessage(user, message, "user");
        }

        private async Task SendMessage(string user, string message, string type)
        {
            if(messages.Count >= messageLimit)
            {
                messages.TryDequeue(out _);
            }
            Message m = new Message()
            {
                User = user,
                Content = SecurityElement.Escape(message),
                UnixTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds,
                Type = type
            };
            messages.Enqueue(m);
            await Clients.All.SendAsync("ReceiveMessage", m);
        }

    }
}
