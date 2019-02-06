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
        public Share Share1 { get; set; }
        public Share Share2 { get; set; }
        public Ticker Ticker { get; set; }

        public UserTests()
        {
            Share1 = new Share()
            {
                NumberOfShares = 100,
                Price = 50,
                Symbol="GOOG"
            };

            Share2 = new Share()
            {
                NumberOfShares = 100,
                Price = 100,
                Symbol = "GOOG"
            };

            Ticker = new Ticker()
            {
                Stocks = new Stock()
                {
                    NumberOfStocks =100,
                    Price= 7.5
                },
                Symbol="GOOG",
                Beta=2.0
            };

            Sut = new User()
            {
                AccountBalance = (double)5000m,
                Portfolio = new Portfolio()
                {
                    Shares = new List<Share>() { Share1,Share2 }
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
            
            Assert.True(Sut.Buy("Google","GOOG",25));
            Assert.True(Sut.Portfolio.Shares.Count == (numberOfShareTypes+1));
            Assert.True(Company.Tickers[0].Stocks.NumberOfStocks < originalStockNumber);


        }

        [Fact]
        public void Test_FailedBuys()
        {
            Assert.False(Sut.Buy("Amazon", "GOOG", 25));
            Assert.False(Sut.Buy("Google", "AMZN", 25));
            Assert.False(Sut.Buy("Google", "GOOG", 150));
        }

        [Fact]
        public void Test_AccountOverDrawn()
        {
            Sut.AccountBalance = 10;
            Assert.False(Sut.Buy("Google", "GOOG", 90));
        }

        [Fact]
        public void Test_PostBuyValues()
        {
            double oldPortfolioValue = Sut.Portfolio.Value;
            double oldBalance = Sut.AccountBalance;

            Sut.Buy("Google", "GOOG", 25);

            Assert.True(oldPortfolioValue < Sut.Portfolio.Value);
            Assert.True(oldBalance > Sut.AccountBalance);
        }

        [Fact]
        public void Test_SellFromAether()
        {
            Assert.False(Sut.Sell("Amazon", "GOOG", 25));
            Assert.False(Sut.Sell("Google", "AMZN", 25));
            Assert.False(Sut.Sell("Google", "GOOG", 500));
        }

        [Fact]
        public void Test_SellFromOne()
        {
            //In this test, we have enough just to stick with share type 1
            var originalShareNumber = Sut.Portfolio.Shares[0].NumberOfShares;
            var originalStockNumber = Company.Tickers[0].Stocks.NumberOfStocks;

            Assert.True(Sut.Sell("Google", "GOOG", 25));
            Assert.True(Sut.Portfolio.Shares[0].NumberOfShares < originalShareNumber);
            Assert.True(Company.Tickers[0].Stocks.NumberOfStocks > originalStockNumber);

        }

        [Fact]
        public void Test_SellFromMany()
        {
            int originalShareTypes = Sut.Portfolio.Shares.Count;
            var originalShareNumber = 0;
            foreach (var item in Sut.Portfolio.Shares)
            {
                originalShareNumber += item.NumberOfShares;
            }
            Assert.True(Sut.Portfolio.Shares[0].NumberOfShares < 175);
            var sellvalidation = Sut.Sell("Google", "GOOG", 175);
            Assert.True(sellvalidation);
            Assert.True(originalShareTypes > Sut.Portfolio.Shares.Count);//This Assert is failing

            var newShareNumber = 0;
            foreach (var item in Sut.Portfolio.Shares)
            {
               newShareNumber += item.NumberOfShares;
            }

            Assert.True(newShareNumber<originalShareNumber);

        }

        [Fact]
        public void Test_PostSellValues()
        {
            double oldPortfolioValue = Sut.Portfolio.Value;
            double oldBalance = Sut.AccountBalance;

            Sut.Sell("Google", "GOOG", 150);

            Assert.True(oldPortfolioValue > Sut.Portfolio.Value);
            Assert.True(oldBalance < Sut.AccountBalance);
        }
    }
}
