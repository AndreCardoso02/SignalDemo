using SignalR_SqlTableDependency.Hubs;
using SignalR_SqlTableDependency.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace SignalR_SqlTableDependency.SubscribeTableDependencies
{
    public class SubscribeProductTableDependency
    {
        SqlTableDependency<Product> tableDependency;
        DashboardHub dashboardHub;

        public SubscribeProductTableDependency(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;
        }

        public void SubscribeTableDependency()
        {
            string connectionString = "Data Source=172.31.6.9;Initial Catalog=SignalRDB;User Id=sa;Password=AT@123;";
            tableDependency = new SqlTableDependency<Product>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Product> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                dashboardHub.SendProducts();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{ nameof(Product) } SqlTableDependency error: { e.Error.Message }");
        }
    }
}
