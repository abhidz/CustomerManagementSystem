using CustomerManagementSystem.AggregateRoot;

namespace CustomerManagementSystem.Entities
{
    public class Factory
    {
        public static CustomerAggregate CreateCustomer(string Name, decimal money, List<Address> addresses)
        {
            var customerAggregate = new CustomerAggregate(Name, money);
            foreach (var address in addresses)
            {
                customerAggregate.AddAddress(address);
            }
            return customerAggregate;
        }
    }
}
