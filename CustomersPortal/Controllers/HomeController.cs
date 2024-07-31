using Microsoft.AspNetCore.Mvc;
using CustomersPortal.Models;
using Newtonsoft.Json;

namespace CustomersPortal.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Customer> lstCustomers = new List<Customer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5124/api/Customers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lstCustomers = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                }
            }
            return View(lstCustomers);
        }
    }
}
