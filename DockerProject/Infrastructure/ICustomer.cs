using DockerProject.Customers;
using System.Collections.Generic;

namespace DockerProject.Infrastructure
{
    public interface ICustomer
    {
        List<CustomerModel> GetCustomerList();
    }
}
