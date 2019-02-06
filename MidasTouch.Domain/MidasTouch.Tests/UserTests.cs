using MidasTouch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MidasTouch.Tests
{
    public class UserTests
    {
        public User Sut {get;set;}
        public Company Company { get; set; }
        public Share Share { get; set; }
        public Ticker Ticker { get; set; }

        public UserTests()
        {
            Share = new Share()
            {
                NumberOfShares = 100,
                Price = (double)50m,
                Symbol="GOOG"
            };

            Ticker = new Ticker()
            {
                Stocks = new Stock()
                {
                    NumberOfStocks =100,
                    Price=(double) 7.5
                },
                Symbol="GOOG",
                Beta=2.0
            };

            Sut = new User()
            {
                AccountBalance = (double)5000m,
                Portfolio = new Portfolio()
                {
                    Shares = new List<Share>() { Share }
                },
                Identity = new Identity()
                {
                    Name= new Name()
                    {
                        First="John",
                        Last="Schmidt"
                    }
                }
            };
            

            Company = new Company()
            {
                Tickers=new List<Ticker>(){Ticker},
                Name="Google"
            };

            Market.Companies = new List<Company>() { Company };

        }

        [Fact]
        public void Test_IsValid()
        {
            Assert.True(Sut.IsValid());
            Assert.True(Company.IsValid());
            foreach (var item in Sut.Portfolio.Shares)
            {
                Assert.True(item.IsValid());
            }

        }

        [Fact]
        public void Test_Buy()
        {
            var numberOfShareTypes = Sut.Portfolio.Shares.Count;
            var originalStockNumber = Company.Tickers[0].Stocks.NumberOfStocks;
            Assert.False(Sut.Buy("Amazon", "GOOG", 25));
            Assert.False(Sut.Buy("Google", "AMZN", 25));
            Assert.False(Sut.Buy("Google", "GOOG", 150));

            Assert.True(Sut.Buy("Google","GOOG",25));
            Assert.True(Sut.Portfolio.Shares.Count == (numberOfShareTypes+1));
            Assert.True(Company.Tickers[0].Stocks.NumberOfStocks < originalStockNumber);

            Sut.AccountBalance = (double) 10m;
            Assert.False(Sut.Buy("Google", "GOOG", 90));

        }

        [Fact]
        public void Test_Sell()
        {
            var originalShareNumber = Sut.Portfolio.Shares[0].NumberOfShares;
            var originalStockNumber = Company.Tickers[0].Stocks.NumberOfStocks;

            Assert.False(Sut.Sell("Google", "GOOG", 150));
            Assert.True(Sut.Sell("Google", "GOOG", 25));
            Assert.True(Sut.Portfolio.Shares[0].NumberOfShares < originalShareNumber);
            Assert.True(Company.Tickers[0].Stocks.NumberOfStocks > originalStockNumber);

        }
    }
}
