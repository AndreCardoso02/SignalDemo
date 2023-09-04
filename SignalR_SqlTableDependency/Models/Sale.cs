namespace SignalR_SqlTableDependency.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public decimal Amount { get; set; }
        public string PurchasedOn { get; set; }
    }
}
