using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Domein.Data.Data
{
    public class Card
    {
        public int Id { get; set; }
        [MaxLength(16)]
        public string CardNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PassworSalt { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateIssue { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime DateExpiry { get; set; }
        public int PasswordTryCount { get; set; } = 0;
        public bool Valid { get; set; } = false;
        public List<Bill> Bill { get; set; } = new();
        public List<BlockCard> BlockCard { get; set; }= new();
        public List<LoggTime> LoggTime { get; set; } = new();

    }
}
