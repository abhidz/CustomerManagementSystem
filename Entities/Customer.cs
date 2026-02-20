using CustomerManagementSystem.ValueObjects;

namespace CustomerManagementSystem.Entities
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Money Money { get; set; }
        public List<Address> Addresses { get; set; } 
    }

    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
    }
}
