using ATM.Bank.Domein.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Domein.Data.Data
{
    public class Address
    {
        public int Id { get; set; }
        public AddressType AddressType { get; set; }
        [Required]
        [MaxLength(100)]
        public string address { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
