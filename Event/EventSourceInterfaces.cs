namespace CustomerManagementSystem.Event
{
    // We will serialize the event to JSON and store it in a database or publish it to a message queue.
    // Hence using Newtonsoft.Json.JsonIgnore to ignore the properties that we don't want to serialize.
    public interface IEvent
    {
        public int Id { get; set; } // Unique identifier for the event, can be used for database storage

        [Newtonsoft.Json.JsonIgnore]
        Guid Guid { get; set; } // Unique identifier for the event

        [Newtonsoft.Json.JsonIgnore]
        int Version { get; set; } // Version of the event, useful for event sourcing

        [Newtonsoft.Json.JsonIgnore]
        string eventType { get; set; } // Type of the event, useful for event sourcing
    }
    public interface IEventRecord: IEvent
    {
        string eventData { get; set; }
    }
    // It is event source
    public interface IEventStore<T>
    {
        List<IEventRecord> GetEvents(Guid aggregateId); // GetEvents method will take an aggregateId as a parameter and return a list of events
        List<IEventRecord> GetAllEvents(); // GetAllEvents method will return a list of all events
        bool AppendEvent(T e); // AppendEvent method will take an aggregateId and an eventRecord as parameters and return a boolean indicating success or failure
    }

    public class CreateCustomerEvent : IEvent
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int Version { get; set; }
        public string eventType { get; set; }
        public string Name { get; set; }
        public decimal Money { get; set; }
        public string User { get; set; } // User who created this event
    }

    public class CreateAdressEvent : IEvent
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int Version { get; set; }
        public string eventType { get; set; }
        public string Street { get; set; }
    }
}
