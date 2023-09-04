using SignalR_SqlTableDependency.Models;
using System.Data;
using System.Data.SqlClient;

namespace SignalR_SqlTableDependency.Repositories
{
    public class CustomerRepository
    {
        string connectionString;

        public CustomerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> persons = new List<Customer>();
            Customer customer;

            var data = GetCustomerDetailsFromDb();

            foreach (DataRow row in data.Rows)
            {
                customer = new Customer
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Name = row["Name"].ToString(),
                    Gender = row["Gender"].ToString(),
                    Mobile = row["Mobile"].ToString()
                };

                persons.Add(customer);
            }

            return persons;
        }

        private DataTable GetCustomerDetailsFromDb()
        {
            var query = "SELECT Id, Name, Gender, Mobile FROM Customer";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
