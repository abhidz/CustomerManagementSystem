using AutoMapper;
using CustomerManagementSystem.Application;
using CustomerManagementSystem.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CreateCustomerHandlers _handler;
        private readonly IMapper _mapper;

        public CustomerController(CreateCustomerHandlers handler, IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }
        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto customerDto)
        {
            var command = new CreateCustomerCommand();
            _mapper.Map(customerDto, command);
            await _handler.Handle(command); // It will call database, event sourcing and publish event to message queue.
            return Ok();
        }
       
    }
}
