using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomersPortal.Pages
{
    public class AddCustomerModel : PageModel
    {
        private readonly ILogger<AddCustomerModel> _logger;

        public AddCustomerModel(ILogger<AddCustomerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
