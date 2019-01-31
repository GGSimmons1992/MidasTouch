using Microsoft.EntityFrameworkCore;
using MidasTouch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidasTouch.Data
{
    class MidasTouchDBContext : DbContext
    {
        public DbSet<FinProduct> FinProducts { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("server=garydotnet2019.database.windows.net;database=MidasTouchDB;user id=sqladmin;password=Florida2019;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FinProduct>().HasKey(e => e.Id);
            builder.Entity<History>().HasKey(e => e.Id);
            builder.Entity<Portfolio>().HasKey(e => e.Id);
            builder.Entity<User>().HasKey(e => e.Id);


            builder.Entity<History>().Ignore(e=>e.PriceHistory);
            builder.Entity<Portfolio>().Ignore(e => e.Stocks);
        }
    }
}
