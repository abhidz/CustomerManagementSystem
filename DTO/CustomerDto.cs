using CustomerManagementSystem.Entities;

namespace CustomerManagementSystem.DTO
{
    public class CustomerDto
    {
        public int ID { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Amount { get; set; }

        public List<Address> Addresses { get; set; }
        public CustomerDto()
        {
            this.Addresses = new List<Address>();
        }
    }

    public class AddressDto
    {
        public string Street { get; set; }
    }
}
