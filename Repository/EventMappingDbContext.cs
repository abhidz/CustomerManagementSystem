using CustomerManagementSystem.Event;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CustomerManagementSystem.Repository
{
    public interface IEventStore<T>
    {
        List<IEventRecord> GetEvents(Guid aggregateId);
        List<IEventRecord> GetEvents();

        bool AppendEvent(T e);
    }
    public interface IEventRecord : IEvent
    {

        string eventData { get; set; }

    }

    public class  EventRecord: IEventRecord
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int Version { get; set; }
        public string eventType { get; set; }
        public string eventData { get; set; }
        public string Status { get; set; }

        public EventRecord()
        {
            Status = "Created";
        }
    }

    public class SqlServerEventDb : IEventStore<IEvent>
    {
        EventDbContext _db = new EventDbContext();

        public bool AppendEvent(IEvent e)
        {
            var er = new EventRecord()
            {
                Guid = e.Guid,
                eventType = e.eventType,
                Version = 1,
                eventData = JsonConvert.SerializeObject(e)
            };

            _db.EventRecords.Add(er);
            _db.SaveChanges();
            return true;
        }

        public List<IEventRecord> GetEvents(Guid aggregateId)
        {
            return (List<IEventRecord>)
                _db.EventRecords.Where(e => e.Guid == aggregateId);
        }

        public List<IEventRecord> GetEvents()
        {
            return _db.EventRecords.ToList<IEventRecord>();

        }
    }
    public class EventDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
    @"Server=.\SQLEXPRESS;Database=CustomerEventDb;Trusted_Connection=True;TrustServerCertificate=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventRecord>().ToTable("tblEvent");
        }
        public DbSet<EventRecord> EventRecords { get; set; }
    }
}
