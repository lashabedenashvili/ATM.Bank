using ATM.Bank.Domein.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Infrastructure.Dto.UserRegistration
{
    public class UserDto
    {
        public string Name { get; set; }        
        public string Surname { get; set; }       
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }       
        public Gender Gender { get; set; }
    }
}
