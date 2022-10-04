using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryApi.Models
{
    public class BookKeeper
    {
        [Key]
        public int BookKeeperId { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public KeepType KeepType { get; set; }
        public int Amount { get; set; }
        public DateTime InsertedDate { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string InsertedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string UpdatedBy { get; set; }
    }
}
