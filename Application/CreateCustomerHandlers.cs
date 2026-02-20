using CustomerManagementSystem.Entities;
using CustomerManagementSystem.Event;
using CustomerManagementSystem.Repository;

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
        public void Handle(CreateCustomerCommand command)
        {
            var aggregatRootInstance = Factory.CreateCustomer(command.Name, command.Money, command.Addresses);
            if (aggregatRootInstance.Validate())
            {
                // Save the aggregate root instance to the repository
                var entity = aggregatRootInstance.GetCustomer();
                IRepository<Customer> respository = new EfCustomer(); // Get the repository instance (e.g., from dependency injection)
                if (respository.Save(entity))
                {
                    Console.WriteLine("Customer created successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to create customer.");
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
