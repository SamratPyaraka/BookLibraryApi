using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorePlayGround.Models
{
    public class Transactions
    {
        [Key]
        public int TransactionID { get; set; }
        [Column(TypeName ="nvarchar(12)")]
        [DisplayName("Account Number")]
        public int AccountNumber { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        [DisplayName("Beneficiary Name")]
        public string BeneficiaryName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Bank Name")]
        public string BankName { get; set; }
        [Column(TypeName = "nvarchar(11)")]
        [DisplayName("IFSC Code")]
        public string IFSCCode { get; set; }
        public int Amount { get; set; }
        public DateTime InsertedDate { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Transaction Type")]
        public string InsertedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string UpdatedBy { get; set; }
    }
}
