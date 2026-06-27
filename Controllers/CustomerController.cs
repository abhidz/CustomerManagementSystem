using AutoMapper;
using CustomerManagementSystem.Application;
using CustomerManagementSystem.DTO;
using MediatR;
using Microsoft.ApplicationInsights;
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
        private readonly TelemetryClient _telemetryClient;

        public CustomerController(IMapper mapper, ILogger<CustomerController> logger,
            IMediator mediator, TelemetryClient telemetryClient)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
            _telemetryClient = telemetryClient;
        }
        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto customerDto)
        {
            _telemetryClient.TrackEvent("Post method is started;");
            var command = new CreateCustomerCommand();
            _mapper.Map(customerDto, command);
            await _mediator.Send(command); // It will call database, event sourcing and publish event to message queue.
            return Ok();
        }
       
    }
}
