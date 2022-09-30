using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Domein.Data.Data
{
    public class BlockCard
    {
        public int Id { get; set; }
        public int CardId { get; set; }     
        
        public DateTime? BlockTime { get; set; }
        public DateTime? UnBlockTime { get; set; }

    }
}
