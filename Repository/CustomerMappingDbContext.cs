using CustomerManagementSystem.Entities;
using CustomerManagementSystem.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystem.Repository
{
    public class CustomerMappingDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
    @"Server=.\SQLEXPRESS;Database=CustomerDb;Trusted_Connection=True;TrustServerCertificate=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var owner = modelBuilder.Entity<Customer>()
                        .ToTable("tblCustomer");
            owner.OwnsOne(c => c.Money, m =>
            {
                m.Property(p => p.Value)
                 .HasColumnName("Amount")
                 .HasPrecision(18, 2);                   // decimal(18,2)

                m.WithOwner();
            });
            modelBuilder.Entity<Address>()
                   .ToTable("tblAddress");
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
