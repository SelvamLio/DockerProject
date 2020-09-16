using DockerProject.Customers;
using DockerProject.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DockerProject.Controllers
{
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerHandler iCustomerHandler;

        public CustomerController(ICustomerHandler _iCustomerHanlder)
        {
            this.iCustomerHandler = _iCustomerHanlder;
        }

        [HttpGet]
        public IActionResult GetCustomerList()
        {

            var custoemr = this.iCustomerHandler.GetAllCustomer();

            if (custoemr == null || custoemr.Count == 0)
            {
                return NotFound();
            }

            return this.Ok(custoemr);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCustomer(string id)
        {
            int customerId;
            if (!int.TryParse(id, out customerId))
            {
                return this.BadRequest();
            }

            var customer = this.iCustomerHandler.GetCustomer(id);

            if (customer == null)
            {
                return this.NotFound();
            }

            return this.Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CustomerModel customer)
        {
            if (customer == null)
            {
                return this.BadRequest();
            }

            var createCustomer = this.iCustomerHandler.CreateCustomer(customer);

            if (createCustomer == "Success")
            {
                return this.Created(string.Empty, customer);
            }

            return this.BadRequest(createCustomer);
        }
    }
}
