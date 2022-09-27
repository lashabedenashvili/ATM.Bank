using ATM.Bank.Domein.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Infrastructure.Dto.UserRegistration
{
    public class AddressDto
    {    
        public AddressType AddressType { get; set; }       
        public string address { get; set; }
    }
}
