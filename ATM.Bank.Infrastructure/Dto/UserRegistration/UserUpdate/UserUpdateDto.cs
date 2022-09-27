using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Infrastructure.Dto.UserRegistration.UserUpdate
{
    public class UserUpdateDto
    {
      
        public UpdateUserDto User { get; set; }
        public AddressDto Address { get; set; }
        public ContactInformationDto ContactInformation { get; set; }
        public PrivateInformationDto PrivateInformation { get; set; }
    }
}
