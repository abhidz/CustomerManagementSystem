using AutoMapper;
using CustomerManagementSystem.Application;
using CustomerManagementSystem.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;
        private readonly IMediator _mediator;

        public CustomerController(IMapper mapper, ILogger<CustomerController> logger, IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }
        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto customerDto)
        {
            var command = new CreateCustomerCommand();
            _mapper.Map(customerDto, command);
            await _mediator.Send(command); // It will call database, event sourcing and publish event to message queue.
            return Ok();
        }
       
    }
}
