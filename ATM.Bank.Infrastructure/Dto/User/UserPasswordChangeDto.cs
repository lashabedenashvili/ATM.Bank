using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Infrastructure.Dto.User
{
    public class UserPasswordChangeDto
    {
        public string PersonalNumber { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
