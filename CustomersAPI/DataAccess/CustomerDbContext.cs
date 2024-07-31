using Microsoft.EntityFrameworkCore;
using CustomersAPI.DataAccess.Entities;

namespace CustomersAPI.DataAccess
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Customer Table
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "User1",
                DateOfBirth = new DateTime(1990, 01, 01),
                EmailAddress = "testuser1@gmail.com",
                PhoneNumber = "1234567890",
                IsActive = true,
            });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "User2",
                DateOfBirth = new DateTime(1991, 01, 01),
                EmailAddress = "testuser2@gmail.com",
                PhoneNumber = "3123456789",
                IsActive = true,
            });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "User3",
                DateOfBirth = new DateTime(1992, 01, 01),
                EmailAddress = "testuser3@gmail.com",
                PhoneNumber = "343343222",
                IsActive = false,
            });
        }
    }
}