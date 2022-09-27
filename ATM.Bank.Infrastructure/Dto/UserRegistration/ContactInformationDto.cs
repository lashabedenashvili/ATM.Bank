using ATM.Bank.Domein.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Infrastructure.Dto.UserRegistration
{
    public class ContactInformationDto
    {
        public ContactInformationType ContactInformationType { get; set; }       
        public string ContactInfo { get; set; }
    }
}
