using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Infrastructure.Dto.UserRegistration
{
    public class UserRegistrationDto
    {
        public UserDto UserDto { get; set; }
        public PrivateInformationDto PrivateInformationDto { get; set; }
        public ContactInformationDto ContactInformationDto { get; set; }
        public AddressDto AddressDto { get; set; }
    }
}
