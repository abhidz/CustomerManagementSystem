using CustomerManagementSystem.Entities;
using CustomerManagementSystem.Event;
using CustomerManagementSystem.Repository;
using System.Text.Json;

namespace CustomerManagementSystem.Application
{
    public class CreateCustomerCommand : ICommand
    {
        public string Id { get; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public decimal Money { get; set; }
        public string User { get; set; } // User who created this command

        public List<Address> Addresses { get; set; } // List of addresses to be added to the customer
        public CreateCustomerCommand()
        {
            Guid = Guid.NewGuid();
            Addresses = new List<Address>();
        }
    }

    public class CreateCustomerHandlers : ICommandHandler<CreateCustomerCommand>
    {
        private readonly IRepository<Customer> _repository;
        private readonly CustomerMappingDbContext _dbContext;

        public CreateCustomerHandlers(IRepository<Customer> repository, CustomerMappingDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public async Task Handle(CreateCustomerCommand command)
        {

            var aggregatRootInstance = Factory.CreateCustomer(command.Name, command.Money, command.Addresses);
            if (aggregatRootInstance.Validate())
            {
                // Save the aggregate root instance to the repository
                var customer = aggregatRootInstance.GetCustomer();
                // Use a DB transaction so Customer and Outbox rows are saved atomically
                try
                {
                    _dbContext.Customers.Add(customer);

                    // create outbox entries for each domain event
                    foreach (var e in aggregatRootInstance.GetEvents())
                    {
                        var payload = JsonSerializer.Serialize(e, e.GetType());
                        var outbox = new OutboxMessage
                        {
                            AggregateId = customer.Id,
                            Type = e.GetType().FullName ?? e.GetType().Name,
                            Payload = payload
                        };
                        _dbContext.OutboxMessages.Add(outbox);
                    }

                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to save customer and outbox messages", ex);
                }

                // Save to Audit log or event sourcing system
                //IEventStore<IEvent> eventDb = new SqlServerEventId();
                var eventDb = new SqlServerEventDb();
                foreach (var item in aggregatRootInstance.GetEvents())
                {
                    if (eventDb.AppendEvent(item))
                    {
                        Console.WriteLine("Event saved successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to save event.");

                    }
                }
            }
        }
    }
}
