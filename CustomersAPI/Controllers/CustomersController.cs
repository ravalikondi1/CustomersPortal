using AutoMapper;
using CustomersAPI.Models;
using CustomersAPI.Services;
using CustomersAPI.Validators;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IValidator _validator;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, ILogger<CustomersController> logger, IValidator validator, IMapper mapper)
        {
            _customerService = customerService;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }
        // GET: api/<CustomersController>
        [HttpGet]
        public IActionResult GetAllCustomers([FromQuery] bool? isActive = null)
        {
            var customerList = _customerService.GetAllCustomers(isActive);
            var customers = _mapper.Map<List<CustomerDTO>>(customerList);
            return Ok(customers);
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(Guid id)
        {
            var customer = _customerService.GetCustomerByID(id);
            if (customer == null)
            {
                return NotFound();
            }
            var customerDetails = _mapper.Map<CustomerDTO>(customer.Result);
            return Ok(customerDetails);
        }

        // POST api/<CustomersController>
        [HttpPost]
        public IActionResult Post([FromBody] AddOrUpdateCustomer customerInfo)
        {
            try
            {
                var validationResult = _validator.Validate(customerInfo);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ErrorMessage);
                }

                var customer = _customerService.AddCustomer(customerInfo);

                if (customer == null)
                {
                    return BadRequest();
                }

                return Ok(new
                {
                    message = "Added Customer Successfully!",
                    CustomerId = customer!.Result!.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] AddOrUpdateCustomer customerInfo)
        {
            var validationResult = _validator.Validate(customerInfo);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ErrorMessage);
            }

            var customer = _customerService.UpdateCustomer(id, customerInfo);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Customer details has been updated successfully!",
                CustomerId = id
            });
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_customerService.DeleteCustomerByID(id).Result)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Customer has been deleted successfully!",
                CustomerId = id
            });
        }
    }
}
