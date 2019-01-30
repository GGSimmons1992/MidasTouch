using System;
using System.Collections.Generic;
using System.Text;

namespace MidasTouch.Domain.Models
{
    public partial class User
    {
        public Portfolio Portfolio { get; set; }
        public string Name { get; set; }
        public double Account { get; set; }

        public User()
        {
            Portfolio = new Portfolio();
        }

        public bool Buy(FinProduct ticker, int shares)
        {
            if (ticker.Shares < shares || Account < (ticker.PurchasePrice * shares))
            {
                return false;
            }

            ticker.Shares -= shares;

            if (Portfolio.Stocks.ContainsKey(ticker.Ticker))
            {
                Portfolio.Stocks[ticker.Ticker] += shares;
            }
            else
            {
                Portfolio.Stocks.Add(ticker.Ticker, shares);
            }

            Account -= (ticker.PurchasePrice * shares);
            return true;
        }

        public bool Sell(FinProduct ticker, int shares)
        {
            if (!(Portfolio.Stocks.ContainsKey(ticker.Ticker)))
            {
                return false;
            }
            if (Portfolio.Stocks[ticker.Ticker] < shares)
            {
                return false;
            }

            Portfolio.Stocks[ticker.Ticker] -= shares;

            if(Portfolio.Stocks[ticker.Ticker] == 0)
            {
                Portfolio.Stocks.Remove(ticker.Ticker);
            }

            ticker.Shares += shares;

            Account += (ticker.Price * shares);
            return true;
        }

    }
}
