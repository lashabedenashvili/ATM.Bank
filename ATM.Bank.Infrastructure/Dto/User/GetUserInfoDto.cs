using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Infrastructure.Dto.User
{
    public class GetUserInfoDto
    {
        public string PersonalNumber { get; set; }
        public string Password { get; set; }
    }
}
