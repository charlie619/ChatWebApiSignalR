using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatWebApiSignalR.Hubs
{
    // The ChatHub class will inherit from Hub, which is part of the SignalR library.
    public class ChatHub : Hub
    {
        // This method will be called by clients to send a message.
        public async Task SendMessage(string user, string message)
        {
            // This line sends the received message to all connected clients.
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendTypingMessage(string message)
        {
            // Broadcast the message to all other clients (excluding the sender)
            await Clients.Others.SendAsync("ReceiveTypingMessage", message);
        }
    }
}
