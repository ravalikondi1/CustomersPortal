using CustomersAPI.Models;

namespace CustomersAPI.Validators
{
    public interface IValidator
    {
        (bool IsValid, string ErrorMessage) Validate(AddOrUpdateCustomer addOrUpdateCustomer);
    }
}
