using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryApi.Models
{
    public class Books
    {
        [Key]
        public int BookID { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        public string BookName { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string BookDescription { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string BookImageURL { get; set; }
        public int BookCount { get; set; }
        public BookType Category { get; set; }
        public KeepType KeepType { get; set; }
        public DateTime InsertedDate { get; set; }
        public string InsertedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }

        public Status Status { get; set; }
    }

    public enum BookType
    {
        Finance,
        Programming,
        Language,
        Story,
        Novels

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
