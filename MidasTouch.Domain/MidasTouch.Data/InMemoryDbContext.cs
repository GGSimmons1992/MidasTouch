using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Text;
using MidasTouch.Domain.Models;

namespace MidasTouch.Data
{
    public class InMemoryDbContext:DbContext
    {

        public DbSet<Company> Companies { get; set; }
        public DbSet<Identity> Identities { get; set; }
        public DbSet<Name> Names { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Ticker> Tickers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName:"TestDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasKey(e => e.Id);
            modelBuilder.Entity<Identity>().HasKey(e => e.Id);
            modelBuilder.Entity<Name>().HasKey(e => e.Id);
            modelBuilder.Entity<Portfolio>().HasKey(e => e.Id);
            modelBuilder.Entity<Share>().HasKey(e => e.Id);
            modelBuilder.Entity<Stock>().HasKey(e => e.Id);
            modelBuilder.Entity<Ticker>().HasKey(e => e.Id);
            modelBuilder.Entity<User>().HasKey(e => e.Id);

        }
    }
}
