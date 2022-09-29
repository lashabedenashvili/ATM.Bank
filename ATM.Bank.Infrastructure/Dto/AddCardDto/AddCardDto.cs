using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Infrastructure.Dto
{
    public class AddCardDto
    {
        public string BillNumber { get; set; }
        public string CardNumber { get; set; }
        public string Password { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateIssue { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateExpiry { get; set; }


        
    }
}
