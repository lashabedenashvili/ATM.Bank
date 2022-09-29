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
        [Required]
        [MaxLength(25)]
        public string BillNumber { get; set; }
        public decimal? Balance { get; set; }
        [Required]
        public int UserId { get; set; }
        public int? CardId { get; set; }
        public Card Card { get; set; }
        public User User { get; set; }
        public List<Transaction> Transaction { get; set; } = new();
    }
}

