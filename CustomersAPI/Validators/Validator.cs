using CustomersAPI.Models;
using System.Text;

namespace CustomersAPI.Validators
{
    public class Validator : IValidator
    {
        private bool IsValid;

        public Validator()
        {
            IsValid = true;
        }

        public (bool IsValid, string ErrorMessage) Validate(AddOrUpdateCustomer addOrUpdateCustomer)
        {
            StringBuilder sb = new StringBuilder();
            if (addOrUpdateCustomer == null)
            {
                return (false, string.Empty);
            }
            if (addOrUpdateCustomer.FirstName == null)
            {
                IsValid = false;
                sb.AppendLine("First Name cannot be empty");
            }
            if (addOrUpdateCustomer.EmailAddress == null)
            {
                IsValid = false;
                sb.AppendLine("Email address cannot be empty");
            }
            return (IsValid, sb.ToString());
        }
    }
}
