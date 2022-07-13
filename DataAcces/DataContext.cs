﻿using BakingAppDataLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces
{
    public class DataContext : IdentityDbContext
    {
         
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users{ get; set; }

    }
}