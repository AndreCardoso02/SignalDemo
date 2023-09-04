using SignalR_SqlTableDependency.Hubs;
using SignalR_SqlTableDependency.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace SignalR_SqlTableDependency.SubscribeTableDependencies
{
    public class SubscribeCustomerTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<Customer> tableDependency;
        DashboardHub dashboardHub;

        public SubscribeCustomerTableDependency(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Customer>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChange;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnChange(object sender, RecordChangedEventArgs<Customer> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                dashboardHub.SendCustomers();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($" {nameof(Customer)} SqlTableDependency error: {e.Error.Message}");
        }
    }
}
