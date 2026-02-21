using CustomerManagementSystem.Entities;

namespace CustomerManagementSystem.Repository
{
    public interface IRepository<T>
    {
        bool Save(T entity); // Save to DB
        bool Add(T entity); // ads in memory
        List<T> GetAll(); // GetAll method will return a list of all entities
        List<T> GetAllById(int id); // GetAllById method will return a list of entities based on the provided ID
    }

    public abstract class RepositoryAbstract<T> : IRepository<T>
    {
        List<T> entities = new List<T>();
        public bool Add(T entity)
        {
            entities.Add(entity);
            return true;
        }

        public abstract List<T> GetAll();

        public abstract List<T> GetAllById(int id);

        public abstract bool Save(T entity);
    }

    public abstract class EFRepositoryAbstract<T> : RepositoryAbstract<T>
    {
       protected readonly CustomerMappingDbContext _dbContext;
        protected EFRepositoryAbstract(CustomerMappingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }

    public class EfCustomer: EFRepositoryAbstract<Customer>
    {
        public EfCustomer(CustomerMappingDbContext dbContext)
       : base(dbContext)
        {
        }
        public override List<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }

        public override List<Customer> GetAllById(int id)
        {
            return _dbContext.Customers
           .Where(x => Convert.ToInt32(x.Id) == id)
           .ToList();
        }

        public override bool Save(Customer entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            _dbContext.Customers.Add(entity);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
