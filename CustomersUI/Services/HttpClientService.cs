using CustomersUI.Models;
using Newtonsoft.Json;
using System.Text;

namespace CustomersUI.Services
{
    public class HttpClientService(IConfiguration configuration) : IHttpClientService
    {
        private readonly string _url = configuration["CustomerAPI:Url"]!;

        public async Task<List<Customer>> GetAllCustomers()
        {
            List<Customer> lstCustomers = [];
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(apiResponse))
                    {
                        lstCustomers = JsonConvert.DeserializeObject<List<Customer>>(apiResponse!);
                    }
                }
            }
            return lstCustomers;
        }

        public async Task<Customer?> GetCustomer(Guid id)
        {
            Customer customer = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_url + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(apiResponse))
                        {
                            customer = JsonConvert.DeserializeObject<Customer>(apiResponse!);
                        }
                    }
                }
            }
            return customer;
        }

        public async Task<bool> AddCustomer(Customer customer)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(_url, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> UpdateCustomer(Guid id, Customer customer)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync(_url + id, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> DeleteCustomer(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(_url + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
