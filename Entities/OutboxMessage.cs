namespace CustomerManagementSystem.Entities
{
    public class OutboxMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string AggregateId { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Payload { get; set; } = default!;
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
        public bool Dispatched { get; set; }
        public DateTime? DispatchedAt { get; set; }
        public int Attempts { get; set; }
    }
}
