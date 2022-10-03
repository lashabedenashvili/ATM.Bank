using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Domein.Data.Data
{
    public class LoggTime
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
        public DateTime LogIn { get; set; }
        public DateTime? LogOut { get; set; }
        
    }
}
