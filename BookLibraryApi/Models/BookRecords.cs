namespace BookLibraryApi.Models
{
    public class BookRecords
    {
        public string? ISBN { get; set; }
        public int BookID { get; set; }
        public string? Title { get; set; }
        public int? Amount { get; set; }
        public KeepType KeepType { get; set; }
        public DateTime? ValidTill { get; set; }
        public DateTime? PurchasedOn { get; set; }
        public bool HasExpiry { get; set; }
        public string? OwnBorrow { get; set; }

    }
}
