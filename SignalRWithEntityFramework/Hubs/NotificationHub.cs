using Microsoft.AspNetCore.SignalR;
using SignalRWithEntityFramework.Models;

namespace SignalRWithEntityFramework.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly SignalRnotificationDbContext dbContext;

        public NotificationHub(SignalRnotificationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        // Verifing when a browser has connect into the web site
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }

        // Save user name
        public async Task SaveUserConnection(string username)
        {
            var connectionId = Context.ConnectionId;
            HubConnection hubConnection = new HubConnection
            {
                ConnectionId = connectionId,
                Username = username
            };

            // Searching if is already exists
            var hubConnectionFound = dbContext
                                       .HubConnections
                                       .FirstOrDefault(hub => hub.Username == username);

            if (hubConnectionFound != null) // update if exists
            {
                hubConnection.ConnectionId = connectionId;
                dbContext.HubConnections.Update(hubConnection);
                await dbContext.SaveChangesAsync();
            } 
            else // Insert if not exists
            {
                dbContext.HubConnections.Add(hubConnection);
                await dbContext.SaveChangesAsync();
            }
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var hubConnection = dbContext
                                .HubConnections
                                .FirstOrDefault(hub => hub.ConnectionId == Context.ConnectionId);

            // Remove before disconnect
            if (hubConnection != null)
            {
                dbContext.HubConnections.Remove(hubConnection);
                dbContext.SaveChangesAsync();
            }

            return base.OnDisconnectedAsync(exception);
        }



    }
}
