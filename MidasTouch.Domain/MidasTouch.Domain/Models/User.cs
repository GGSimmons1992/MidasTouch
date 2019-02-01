using MidasTouch.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidasTouch.Domain.Models
{
  public class User: AThing
  {
    public Portfolio Portfolio { get; set; }
    public Identity Identity { get; set; }
    public double AccountBalance { get; set; }

    public User()
    {
      Portfolio = new Portfolio();
    }

    public bool Buy(string name, string ticker, int shares)
    {
      var c = Market.Companies.Where(s => s.Name == name).FirstOrDefault();
      if (c == null)
      {
        return false;
      }

      var t = c.Tickers.Where(s => s.Symbol == ticker).FirstOrDefault();
      if (t == null)
      {
          return false;
      }

      if (t.Stocks.NumberOfStocks < shares || AccountBalance < (t.Stocks.Price * shares))
      {
        return false;
      }

      t.Stocks.NumberOfStocks -= shares;
      var sh = new Share() { NumberOfShares = shares, Symbol = ticker, Price = t.Stocks.Price };

      if (Portfolio.Shares.Contains(sh))
      {
          Portfolio.Shares[] += shares;
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
