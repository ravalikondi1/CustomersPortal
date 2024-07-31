namespace CustomersPortal.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
    }
}
