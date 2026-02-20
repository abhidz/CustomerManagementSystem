using CustomerManagementSystem.Entities;
using CustomerManagementSystem.Event;
using CustomerManagementSystem.ValueObjects;

namespace CustomerManagementSystem.AggregateRoot
{
    public class CustomerAggregate
    {
        private Customer _customer = null;
        private List<IEvent> _events = new List<IEvent>();
        private readonly Guid GuidForEvent = System.Guid.NewGuid();
        private  int IdForEvent = 0;
        private  int VersionForEvent = 0;

        public List<IEvent> GetEvents()
        {
            return _events.ToList();
        }

        public CustomerAggregate(string Name, decimal money)
        {
            _customer = new Customer();
            _customer.Name = Name;
            _customer.Money = new Money(money);
            _customer.Addresses = new List<Address>();
            _events.Add(new CreateCustomerEvent
            {
                Guid = GuidForEvent,
                Id = ++IdForEvent,
                Version = ++VersionForEvent,
                User = "System", 
                Name = Name,
                Money = money,
                eventType = nameof(CreateCustomerEvent)
            });
        }

        public bool AddAddress(Address obj)
        {
            if (this._customer.Addresses.Count > 3)
            {
                throw new Exception("Cannot add more than 3 addresses");
            }
            this._customer.Addresses.Add(obj);
            _events.Add(new CreateAdressEvent
            {
                Guid = GuidForEvent,
                Id = ++IdForEvent,
                Version = ++VersionForEvent,
                Street = obj.Street,
                eventType = nameof(CreateAdressEvent)
            });
            return true;
        }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(this._customer.Name))
            {
                throw new Exception("Name cannot be empty");
            }
            if (this._customer.Money.Value < 0)
            {
                throw new Exception("Money cannot be negative");
            }
            return true;
        }

        public Customer GetCustomer()
        {
            return this._customer;
        }
    }
}
