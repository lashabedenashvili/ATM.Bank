using ATM.Bank.Domein.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Domein.Data.Data
{
    public class PrivateInformation
    {
        public int Id { get; set; }
        [Required]
        public DocumentType DocumenType { get; set; }

        [MaxLength(10)]
        public string DocumentNumber { get; set; }

        [MaxLength(10)]
        public string SerialNumber { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime DateIssue { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime DateExpiry { get; set; }
        [Required]
        [MaxLength(50)]
        public string PrivateNumber { get; set; }

        [MaxLength(100)]
        public string IssuingAutority { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
