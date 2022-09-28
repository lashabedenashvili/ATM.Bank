using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Domein.Data.Data
{
    public class Transaction
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal DebitEmount { get; set; }
        public decimal CreditEmount { get; set; }
        public Bill Bill { get; set; }

    }
}
