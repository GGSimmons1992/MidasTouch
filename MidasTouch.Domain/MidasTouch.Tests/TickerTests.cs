using MidasTouch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MidasTouch.Tests
{
   public class TickerTests
    {
        public Ticker Sut { get; set; }

        public TickerTests()
        {
            Sut = new Ticker()
            {
                Beta = 1.2,
                Stocks = new Stock()
                {
                    Price = 50.0,
                    NumberOfStocks=50
                },
                Symbol="MSFT"
            };
        }

        [Fact]
        public void Test_IsValid()
        {
            Assert.True(Sut.IsValid());
        }

        [Fact]
        public void Test_Flux()
        {
            var baseprice = Sut.Stocks.Price;
            Sut.Flux();
            Assert.True(Sut.Stocks.Price >= (0.8 * baseprice));
            Assert.True(Sut.Stocks.Price <= (1.2 * baseprice));
        }
    }
}
