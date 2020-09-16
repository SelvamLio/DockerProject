using DockerProject.Customers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace DockerProject.Controllers
{
    [Route("api/rediscustomer")]
    public class RedisCustomerController : ControllerBase
    {
        private readonly IDatabase database;
        public RedisCustomerController(IDatabase _database)
        {
            this.database = _database;
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCustomer(string id)
        {
            var customer = database.StringGet(id);
            if (!customer.HasValue)
            {
                return this.NotFound();
            }

            var encodedCustomer = System.Text.Encoding.UTF8.GetString(customer);
            var jsonCustomer = JsonConvert.DeserializeObject<CustomerModel>(encodedCustomer);

            if (jsonCustomer == null)
            {
                return this.NotFound();
            }

            return this.Ok(jsonCustomer);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CustomerModel customer)
        {
            if (customer == null)
            {
                return this.BadRequest();
            }

            var getCustomer = database.StringGet(customer.Id);
            if (getCustomer.HasValue)
            {
                return this.BadRequest("Customer Id already exist");
            }

            var jsonCustomer = JsonConvert.SerializeObject(customer);
            var createCustomer = this.database.StringSet(customer.Id, jsonCustomer);

            return this.Created(string.Empty, customer);
        }
    }
}
