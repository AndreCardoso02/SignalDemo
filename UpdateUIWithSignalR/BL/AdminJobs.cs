using UpdateUIWithSignalR.Hubs;

namespace UpdateUIWithSignalR.BL
{
    public class AdminJobs
    {

        private AdminHub adminHub;

        public AdminJobs(AdminHub adminHub)
        {
            this.adminHub = adminHub;
        }

        public async Task ProcessLoans()
        {
            var message = "Retrieving loans";
            var status = "started";

             await adminHub.SendJobStatus(message, status);
        }
    }
}
