using CustomerManagementSystem.Application;
using CustomerManagementSystem.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CreateCustomerHandlers _handler;

        public CustomerController(CreateCustomerHandlers handler)
        {
            _handler = handler;
        }
        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto customerDto)
        {
            var command = new CreateCustomerCommand();
            command.Name = customerDto.Name;
            command.Money = decimal.Parse(customerDto.Amount);
            foreach (var address in customerDto.Addresses)
            {
                command.Addresses.Add(address);
            }
            await _handler.Handle(command); // It will call database, event sourcing and publish event to message queue.
            return Ok();
        }
       
    }
}
