using ATM.Bank.Domein.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Domein.Data.Domein
{
    public interface IContext
    {
        public DbSet<User> user { get; set; }
        public DbSet<Bill> bill { get; set; }

        int SaveChanges();
    }
}
