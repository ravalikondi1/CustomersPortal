using CustomersPortal.Models;

namespace CustomersPortal.Services
{
    public interface IHttpClientService
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomer(int id);
    }
}
