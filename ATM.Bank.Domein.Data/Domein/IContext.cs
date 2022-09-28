﻿using ATM.Bank.Domein.Data.Data;
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
        public DbSet<Address> address { get; set; }
        public DbSet<ContactInformation> contactInformation { get; set; }
        public DbSet<PrivateInformation> privateInformation { get; set; }
        public DbSet<Transaction> transaction { get; set; }
        Task<int> SaveChangesAsync();

        int SaveChanges();
    }
}
