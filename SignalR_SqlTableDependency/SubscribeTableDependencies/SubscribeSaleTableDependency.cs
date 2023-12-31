﻿using SignalR_SqlTableDependency.Hubs;
using SignalR_SqlTableDependency.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace SignalR_SqlTableDependency.SubscribeTableDependencies
{
    public class SubscribeSaleTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<Sale> tableDependency;
        DashboardHub dashboardHub;

        public SubscribeSaleTableDependency(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Sale>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChange;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnChange(object sender, RecordChangedEventArgs<Sale> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                dashboardHub.SendSales();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Sale)} SqlTableDependency error: { e.Error.Message }");
        }
    }
}
