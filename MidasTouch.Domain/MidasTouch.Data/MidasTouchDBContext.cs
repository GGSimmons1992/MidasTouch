using Microsoft.EntityFrameworkCore;
using MidasTouch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;

namespace MidasTouch.Data
{
    public class MidasTouchDBContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public MidasTouchDBContext()
        {
        }

        public MidasTouchDBContext(IConfiguration config)
        {
            Configuration = config;
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Identity> Identities { get; set; }
        public DbSet<Name> Names { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Ticker> Tickers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            optionsbuilder.UseSqlServer("server=garydotnet2019.database.windows.net;database=MidasTouchDB; Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Company>().HasKey(e => e.Id);
            modelbuilder.Entity<Identity>().HasKey(e => e.Id);
            modelbuilder.Entity<Name>().HasKey(e => e.Id);
            modelbuilder.Entity<Portfolio>().HasKey(e => e.Id);
            modelbuilder.Entity<Share>().HasKey(e => e.Id);
            modelbuilder.Entity<Stock>().HasKey(e => e.Id);
            modelbuilder.Entity<Ticker>().HasKey(e => e.Id);
            modelbuilder.Entity<User>().HasKey(e => e.Id);

        }
    }
}
