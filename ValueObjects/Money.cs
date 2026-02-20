namespace CustomerManagementSystem.ValueObjects
{
    public record Money
    {
        public decimal Value { get; set; }
        public Money()
        {
            
        }

        public Money(decimal value)
        {
            Value = value;
        }
    }
}
