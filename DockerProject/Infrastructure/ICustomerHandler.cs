using DockerProject.Customers;
using System.Collections.Generic;

namespace DockerProject.Infrastructure
{
    public interface ICustomerHandler
    {
        CustomerModel GetCustomer(string id);

        string CreateCustomer(CustomerModel data);
        
        List<CustomerModel> GetAllCustomer();
    }
}
