using DockerProject.Infrastructure;
using System.Collections.Generic;

namespace DockerProject.Customers
{
    public class Customer : ICustomer
    {

        private static List<CustomerModel> customersList = new List<CustomerModel>();

        public Customer()
        {
            if (customersList.Count == 0)
            {
                CreateCustomer();
            }
        }

        private void CreateCustomer()
        {
            customersList.Add(
                new CustomerModel
                {
                    Id = "1",
                    Name = "Messi",
                    Age = "31",
                    City = "Catalonia",
                    State = "Barcelona",
                    Country = "Spain"
                });

            customersList.Add(new CustomerModel
            {
                Id = "2",
                Name = "Cristiano",
                Age = "33",
                City = "Rome",
                State = "Juventus",
                Country = "Italy"
            });

            customersList.Add(new CustomerModel
            {
                Id = "3",
                Name = "Neymer",
                Age = "29",
                City = "Paris",
                State = "Paris",
                Country = "France"
            });

            customersList.Add(new CustomerModel
            {
                Id = "4",
                Name = "Kane",
                Age = "30",
                City = "London",
                State = "Tottenham",
                Country = "England"
            });
        }

        public List<CustomerModel> GetCustomerList()
        {
            return customersList;
        }
    }
}
