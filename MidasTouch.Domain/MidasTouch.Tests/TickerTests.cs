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

       
    }
}
