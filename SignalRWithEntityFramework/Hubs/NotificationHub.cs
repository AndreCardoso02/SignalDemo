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

        #region Connect, Save User and Disconnect Methods
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

            dbContext.HubConnections.Add(hubConnection);
            await dbContext.SaveChangesAsync();
            
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
        #endregion

        // Sendind Message Methods
        #region Sending Message Methods
        public async Task SendNotificationToAll(string message)
        {
            await Clients.All.SendAsync("ReceivedNotification", message);
        }

        public async Task SendNotificationToClient(string message, string username)
        {
            var listOfHubConnections = dbContext.HubConnections.Where(hub => hub.Username == username).ToList();
            foreach (var hubConnection in listOfHubConnections)
            {
               await  Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", message, username);
            }
        }
        #endregion



    }
}
