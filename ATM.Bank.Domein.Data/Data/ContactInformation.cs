using ATM.Bank.Domein.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Domein.Data.Data
{
    public class ContactInformation
    {
        public int Id { get; set; }
        [Required]
        public ContactInformationType ContactInformationType { get; set; }
        [Required]
        [MaxLength(100)]
        public string ContactInfo { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
