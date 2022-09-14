using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Infrastructure.Dto
{
    public class BillAddDto
    {
        public string personalNumber { get; set; }
        public string BillNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
