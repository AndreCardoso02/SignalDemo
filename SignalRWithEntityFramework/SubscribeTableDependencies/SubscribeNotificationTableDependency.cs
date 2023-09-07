using SignalRWithEntityFramework.Hubs;
using SignalRWithEntityFramework.Models;
using TableDependency.SqlClient;

namespace SignalRWithEntityFramework.SubscribeTableDependencies
{
    public class SubscribeNotificationTableDependency : ISubscribeTableDependency
    {
        private SqlTableDependency<Notification> tableDependency;
        private NotificationHub notificationHub;

        public SubscribeNotificationTableDependency(NotificationHub notificationHub)
        {
            this.notificationHub = notificationHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Notification>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;

        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($" {nameof(Notification)} SqlTableDependency error: {e.Error.Message}"); 
        }

        private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Notification> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var notification = e.Entity;
                if (notification.MessageType == "All")
                {
                    await notificationHub.SendNotificationToAll(notification.Message);
                }
                else if (notification.MessageType == "Personal")
                {
                    await notificationHub.SendNotificationToClient(notification.Message, notification.Username);
                }
            }
        }
    }
}
