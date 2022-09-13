using ATM.Bank.Domein.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Domein.Data.Domein
{
    public class Context:DbContext,IContext
    {
        public DbSet<User> user { get; set; }
       

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }


    } 
}
