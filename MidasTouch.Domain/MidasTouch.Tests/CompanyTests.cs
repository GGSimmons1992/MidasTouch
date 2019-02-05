using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MidasTouch.Domain.Models;

namespace MidasTouch.Tests
{
    public class CompanyTests
    {
        public Company Sut {get;set;}
        
        public CompanyTests()
        {
            var ticker1=new Ticker()
            {
                Stocks=new Stock()
                {
                    NumberOfStocks=100,
                    Price=(double)50m
                }
            };

            var ticker2=new Ticker()
            {
                Stocks=new Stock()
                {
                    NumberOfStocks=300,
                    Price=(double)10m
                }
            };

            Sut= new Company()
            {
                Tickers=new List<Ticker>(){ticker1,ticker2}
            };
        }

        [Fact]
        public void Test_GetCap()
        {
            Assert.True(Sut.MarketCap== (double)(5000m+3000m));
        }
    }
}
