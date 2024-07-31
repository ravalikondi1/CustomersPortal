using System.ComponentModel.DataAnnotations;

namespace CustomersAPI.Models
{
    public class AddOrUpdateCustomer
    {
        [Required]
        public required string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
