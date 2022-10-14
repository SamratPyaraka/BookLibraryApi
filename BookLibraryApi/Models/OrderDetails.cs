namespace BookLibraryApi.Models
{
    public class OrderDetails
    {
        public int UserID { get; set; }
        public int BookID { get; set; }
        public int Amount { get; set; }
        public KeepType KeepType { get; set; }
        public DateTimeOffset Expiry { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PinCode { get; set; }

    }
}
