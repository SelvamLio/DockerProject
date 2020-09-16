using DockerProject.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DockerProject.Customers
{
    public class CustomerHanlder : ICustomerHandler
    {
        private readonly IDistributedCache database;
        private readonly ICustomer iCustomer;

        public CustomerHanlder(IDistributedCache _database, ICustomer _iCustomer)
        {
            this.database = _database;
            this.iCustomer = _iCustomer;
        }

        public List<CustomerModel> GetAllCustomer()
        {
            var customer = iCustomer.GetCustomerList();
            if (customer == null)
            {
                return null;
            }

            return customer;
        }

        public CustomerModel GetCustomer(string id)
        {
            var redisCustomer = database.GetString(id);
            if (redisCustomer != null)
            {
                var jsonCustomer = JsonConvert.DeserializeObject<CustomerModel>(redisCustomer);

                return jsonCustomer;
            }

            var customer = iCustomer.GetCustomerList().Where(c => c.Id.ToString() == id).FirstOrDefault();
            if (customer == null)
            {
                return null;
            }

            var jsonCreateCustomer = JsonConvert.SerializeObject(customer);
            var cacheTime = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(1)
            };

            this.database.SetString(customer.Id, jsonCreateCustomer, cacheTime);

            return new CustomerModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Age = customer.Age,
                City = customer.City,
                State = customer.State,
                Country = customer.Country
            };
        }

        public string CreateCustomer(CustomerModel customer)
        {
            try
            {
                var getCustomer = database.Get(customer.Id);
                if (getCustomer != null)
                {
                    return "Customer Id already exist";
                }

                var existCustomer = iCustomer.GetCustomerList().Where(c => c.Id == customer.Id).FirstOrDefault();
                if (existCustomer != null)
                {
                    return "Customer Id already exist";
                }

                iCustomer.GetCustomerList().Add(customer);

                var jsonCustomer = JsonConvert.SerializeObject(customer);
                var cacheTime = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(1)
                };
                
                this.database.SetString(customer.Id, jsonCustomer, cacheTime);

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
