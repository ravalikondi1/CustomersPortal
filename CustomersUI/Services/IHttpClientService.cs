using CustomersUI.Models;

namespace CustomersUI.Services
{
    public interface IHttpClientService
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomer(Guid id);
        Task<bool> AddCustomer(Customer customer);
        Task<bool> UpdateCustomer(Guid id, Customer customer);
        Task<bool> DeleteCustomer(Guid id);
    }
}
