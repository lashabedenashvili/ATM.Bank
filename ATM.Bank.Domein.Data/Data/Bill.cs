using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Domein.Data.Data
{
    public class Bill
    {

        public int Id { get; set; }
        [MaxLength(25)]
        public string BillNumber { get; set; }
        public decimal Balance { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}

