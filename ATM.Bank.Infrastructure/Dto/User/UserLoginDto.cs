using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Infrastructure.Dto.User
{
    public class UserLoginDto
    {
        public string PersonalNumber { get; set; }
        public string Password { get; set; }
    }
}
