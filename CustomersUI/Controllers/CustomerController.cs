using CustomersUI.Models;
using CustomersUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomersUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IHttpClientService _httpClientService;

        public CustomerController(IHttpClientService httpClientService, ILogger<CustomerController> logger)
        {
            _httpClientService = httpClientService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            List<Customer> lstCustomers = new List<Customer>();
            lstCustomers = await _httpClientService.GetAllCustomers();
            return View(lstCustomers);
        }

        public ViewResult GetCustomer() => View();

        [HttpPost]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            Customer customer = new Customer();
            customer = await _httpClientService.GetCustomer(id);
            return View(customer);
        }
        public ViewResult AddCustomer() => View();

        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customerInfo)
        {
            var response = await _httpClientService.AddCustomer(customerInfo);
            return View(response);
        }

        public async Task<IActionResult> UpdateCustomer(Guid id)
        {
            Customer customer = new Customer();
            customer = await _httpClientService.GetCustomer(id);
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(Guid id, Customer customer)
        {
            await _httpClientService.UpdateCustomer(id, customer);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            await _httpClientService.DeleteCustomer(id);

            return RedirectToAction("Index");
        }
    }
}
