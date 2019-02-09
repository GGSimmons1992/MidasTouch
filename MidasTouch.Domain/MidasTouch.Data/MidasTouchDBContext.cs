﻿using Microsoft.EntityFrameworkCore;
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

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Configuration.GetConnectionString("MidasTouchDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Company>().HasKey(e => e.Id);
            builder.Entity<Identity>().HasKey(e => e.Id);
            builder.Entity<Name>().HasKey(e => e.Id);
            builder.Entity<Portfolio>().HasKey(e => e.Id);
            builder.Entity<Share>().HasKey(e => e.Id);
            builder.Entity<Stock>().HasKey(e => e.Id);
            builder.Entity<Ticker>().HasKey(e => e.Id);
            builder.Entity<User>().HasKey(e => e.Id);

        }
    }
}