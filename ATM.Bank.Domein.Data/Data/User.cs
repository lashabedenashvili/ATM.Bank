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
    public class User
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }              
        [Required]
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public List<Bill> Bill { get; set; } = new();
        public List<ContactInformation> ContactInformation { get; set; } = new();
        public List<PrivateInformation> PrivateInformation { get; set; } = new();
        public List<Address> Address { get; set; } = new();
       
    }
}
