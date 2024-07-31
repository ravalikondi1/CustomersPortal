using CustomersAPI.DataAccess.Entities;
using CustomersAPI.Models;

namespace CustomersAPI.Services
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers(bool? isActive);

        Task<Customer?> GetCustomerByID(Guid id);

        Task<Customer> AddCustomer(AddOrUpdateCustomer customerInfo);

        Task<Customer?> UpdateCustomer(Guid id, AddOrUpdateCustomer customerInfo);

        Task<bool> DeleteCustomerByID(Guid id);
    }
}
