using Microsoft.AspNetCore.SignalR;
using SignalR_SqlTableDependency.Repositories;

namespace SignalR_SqlTableDependency.Hubs
{
    public class DashboardHub : Hub
    {
        ProductRepository productRepository;
        SaleRepository saleRepository;
        CustomerRepository customerRepository;

        public DashboardHub(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            productRepository = new ProductRepository(connectionString);
            saleRepository = new SaleRepository(connectionString);
            customerRepository = new CustomerRepository(connectionString);
        }

        public async Task SendProducts()
        {
            var products = productRepository.GetProducts();
            await Clients.All.SendAsync("ReceivedProducts", products);
        }

        public async Task SendSales()
        {
            var sales = saleRepository.GetSales();
            await Clients.All.SendAsync("ReceivedSales", sales);
        }

        public async Task SendCustomers()
        {
            var customers = customerRepository.GetCustomers();
            await Clients.All.SendAsync("ReceivedCustomers", customers);
        }
    }
}
