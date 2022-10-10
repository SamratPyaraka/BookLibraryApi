using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryApi.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? UserName { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? Password { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string? Email { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string? FirstName { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string? LastName { get; set; }
        public DateTime? Created { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string? CreatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string? LastUpdatedBy { get; set; }
        public Status Status { get; set; }

    }
}
