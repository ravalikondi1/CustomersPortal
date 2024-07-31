using CustomersAPI.DataAccess;
using CustomersAPI.DataAccess.Entities;
using CustomersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomersAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private CustomerDbContext _dbContext;
        private readonly List<Customer> _customersList;
        public CustomerService(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Customer> GetAllCustomers(bool? isActive)
        {
            return isActive == null ?  _dbContext.Customers.ToList() :  _dbContext.Customers.Where(customer => customer.IsActive == isActive).ToList();
        }

        public async Task<Customer?> GetCustomerByID(Guid id)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
        }

        public async Task<Customer> AddCustomer(AddOrUpdateCustomer customerInfo)
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = customerInfo.FirstName,
                LastName = customerInfo.LastName,
                DateOfBirth = customerInfo.DateOfBirth,
                EmailAddress = customerInfo.EmailAddress,
                PhoneNumber = customerInfo.PhoneNumber,
                IsActive = customerInfo.IsActive,
            };

            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer?> UpdateCustomer(Guid id, AddOrUpdateCustomer customerInfo)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer != null)
            {
                customer.FirstName = customerInfo.FirstName;
                customer.LastName = customerInfo.LastName;
                customer.DateOfBirth = customerInfo.DateOfBirth;
                customer.EmailAddress = customerInfo.EmailAddress;
                customer.PhoneNumber = customerInfo.PhoneNumber;
                customer.IsActive = customerInfo.IsActive;

                await _dbContext.SaveChangesAsync();
                return customer;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> DeleteCustomerByID(Guid id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
