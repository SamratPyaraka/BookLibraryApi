using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryApi.Models
{
    public class Books
    {
        [Key]
        public int BookID { get; set; }

        [Column(TypeName ="nvarchar(1000)")]
        public string? Title { get; set; }

        [Column(TypeName = "nvarchar(1200)")]
        public string? Subtitle { get; set; }

        [Column(TypeName = "nvarchar(4000)")]
        public string? Description { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string? ISBN13 { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string? ISBN10 { get; set; }

        [Column(TypeName = "nvarchar(1200)")]
        public string? Authors { get; set; }

        [Column(TypeName = "nvarchar(1200)")]
        public string? Category { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string? ImageURL { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? PublishedYear { get; set; }
        public int? BookCount { get; set; }
        public float? AverageRating { get; set; }
        public int? NumberOfPages { get; set; }
        public int? RatingsCount { get; set; }
        public KeepType KeepType { get; set; }
        public DateTime? InsertedDate { get; set; }
        public string? InsertedBy { get; set; } = "";
        public DateTime? LastUpdatedDate { get; set; }
        public string? LastUpdatedBy { get; set; } = "";

        public Status Status { get; set; }
    }

    

    public enum KeepType
    {
        Rent,
        Purchase
    }

    public enum Status
    {
        Active,
        Inactive
    }

}
