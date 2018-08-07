using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Collections.Concurrent;

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
            await Clients.Others.SendAsync("UserDisconnected", Context.ConnectionId);
            await SendMessage("System", $"{Context.ConnectionId} has left the room.", "system");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            await SendMessage(user, message, "user");
        }

        private async Task SendMessage(string user, string message, string userType)
        {
            if(messages.Count >= messageLimit)
            {
                messages.TryDequeue(out _);
            }
            Message m = new Message()
            {
                User = user,
                Content = message,
                LocalTime = DateTime.Now.ToLocalTime().ToShortTimeString(),
                UserType = userType
            };
            messages.Enqueue(m);
            Console.WriteLine(messages.Count);
            await Clients.All.SendAsync("ReceiveMessage", m);
        }

    }
}
