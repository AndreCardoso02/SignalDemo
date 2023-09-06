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
            // should stop right here if any process fail
            var status = await RetrieveLoans();
            if (!status) return;

            status = await CalculateInterest();
            if (!status) return;

            status = await UpdateLoans();
            if (!status) return;

            status = await SaveLogs();
            if (!status) return;

            status = await SendEmails();
            if (!status) return;
        }

        #region Process
        // Retrieve
        public async Task<bool> RetrieveLoans()
        {
            var type = "Retrieve";

            var message = "Retrieving loans";
            var status = "started";

            await adminHub.SendJobStatus(type, message, status);

            // here is where you put your jobs
            int delayInMS = GetRandomDelay();
            Task.Delay(delayInMS).Wait();

            status = "completed";

            if(status == "completed")
                message = $"Retrieved the loans. ({delayInMS} miliseconds)";
            else
                message = "Failed to retrieve the loans.";

            await adminHub.SendJobStatus(type, message, status);

            if (status == "completed")
                return true;
            else return false;
        }

        // Calculate
        public async Task<bool> CalculateInterest()
        {
            var type = "Calculate";

            var message = "Calculating interest";
            var status = "started";

            await adminHub.SendJobStatus(type, message, status);

            // here is where you put your jobs
            int delayInMS = GetRandomDelay();
            Task.Delay(delayInMS).Wait();

            status = "completed";

            if (status == "completed")
                message = $"Calculated interest for loans. ({delayInMS} miliseconds)";
            else
                message = "Failed to calculate the interest.";

            await adminHub.SendJobStatus(type, message, status);

            if (status == "completed")
                return true;
            else return false;
        }

        // Update
        public async Task<bool> UpdateLoans()
        {
            var type = "Update";

            var message = "Updating loans";
            var status = "started";

            await adminHub.SendJobStatus(type, message, status);

            // here is where you put your jobs
            int delayInMS = GetRandomDelay();
            Task.Delay(delayInMS).Wait();

            status = "completed";

            if (status == "completed")
                message = $"Updated the loans. ({delayInMS} miliseconds)";
            else
                message = "Failed to update the loans.";

            await adminHub.SendJobStatus(type, message, status);

            if (status == "completed")
                return true;
            else return false;
        }

        // Logs
        public async Task<bool> SaveLogs()
        {
            var type = "Logs";

            var message = "Saving logs";
            var status = "started";

            await adminHub.SendJobStatus(type, message, status);

            // here is where you put your jobs
            int delayInMS = GetRandomDelay();
            Task.Delay(delayInMS).Wait();

            status = "completed";

            if (status == "completed")
                message = $"Saved the logs. ({delayInMS} miliseconds)";
            else
                message = "Failed to save the logs.";

            await adminHub.SendJobStatus(type, message, status);

            if (status == "completed")
                return true;
            else return false;
        }

        // Send Emails
        public async Task<bool> SendEmails()
        {
            var type = "Emails";

            var message = "Sending emails";
            var status = "started";

            await adminHub.SendJobStatus(type, message, status);

            // here is where you put your jobs
            int delayInMS = GetRandomDelay();
            Task.Delay(delayInMS).Wait();

            status = "completed";

            if (status == "completed")
                message = $"Emails are sent. ({delayInMS} miliseconds)";
            else
                message = "Failed to send the emails.";

            await adminHub.SendJobStatus(type, message, status);

            if (status == "completed")
                return true;
            else return false;
        }
        #endregion

        #region Util
        // Generating random Delay Min to simulate that is processing and that each process takes different processment time
        private int GetRandomDelay()
        {
            int min = 2000;
            int max = 5000;

            Random random = new Random();
            return random.Next(min, max);
        }
        #endregion
    }
}
