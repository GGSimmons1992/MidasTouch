﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MidasTouch.Data;

namespace MidasTouch.Data.Migrations
{
    [DbContext(typeof(MidasTouchDBContext))]
    [Migration("20190214015257_first_migration")]
    partial class first_migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MidasTouch.Domain.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("MidasTouch.Domain.Models.Identity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<int?>("NameId");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.HasIndex("NameId");

                    b.ToTable("Identities");
                });

            modelBuilder.Entity("MidasTouch.Domain.Models.Name", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("First");

                    b.Property<string>("Last");

                    b.HasKey("Id");

                    b.ToTable("Names");
                });

            modelBuilder.Entity("MidasTouch.Domain.Models.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("MidasTouch.Domain.Models.Share", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NumberOfShares");

                    b.Property<int?>("PortfolioId");

                    b.Property<double>("Price");

                    b.Property<string>("Symbol");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("Shares");
                });

            modelBuilder.Entity("MidasTouch.Domain.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NumberOfStocks");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("MidasTouch.Domain.Models.Ticker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Beta");

                    b.Property<int?>("CompanyId");

                    b.Property<int?>("StocksId");

                    b.Property<string>("Symbol");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("StocksId");

                    b.ToTable("Tickers");
                });

            modelBuilder.Entity("MidasTouch.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AccountBalance");

                    b.Property<int?>("IdentityId");

                    b.Property<int?>("PortfolioId");

                    b.HasKey("Id");

                    b.HasIndex("IdentityId");

                    b.HasIndex("PortfolioId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MidasTouch.Domain.Models.Identity", b =>
                {
                    b.HasOne("MidasTouch.Domain.Models.Name", "Name")
                        .WithMany()
                        .HasForeignKey("NameId");
                });

            modelBuilder.Entity("MidasTouch.Domain.Models.Share", b =>
                {
                    b.HasOne("MidasTouch.Domain.Models.Portfolio")
                        .WithMany("Shares")
                        .HasForeignKey("PortfolioId");
                });

            modelBuilder.Entity("MidasTouch.Domain.Models.Ticker", b =>
                {
                    b.HasOne("MidasTouch.Domain.Models.Company")
                        .WithMany("Tickers")
                        .HasForeignKey("CompanyId");

                    b.HasOne("MidasTouch.Domain.Models.Stock", "Stocks")
                        .WithMany()
                        .HasForeignKey("StocksId");
                });

            modelBuilder.Entity("MidasTouch.Domain.Models.User", b =>
                {
                    b.HasOne("MidasTouch.Domain.Models.Identity", "Identity")
                        .WithMany()
                        .HasForeignKey("IdentityId");

                    b.HasOne("MidasTouch.Domain.Models.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioId");
                });
#pragma warning restore 612, 618
        }
    }
}