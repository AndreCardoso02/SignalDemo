using Microsoft.AspNetCore.SignalR;

namespace SignalRBasics.Hubs
{
    public class ChatHub : Hub
    {
        // Send Message to Clients, calling a function on client side ReceiveMessage
        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
