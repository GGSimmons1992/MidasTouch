using MidasTouch.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidasTouch.Domain.Models
{
  public class Company : AThing
  {
    public string Name { get; set; }
    public List<Ticker> Tickers { get; set; }
    public double MarketCap
    {
      get
      {
        double c = 0;
        foreach (Ticker item in Tickers)
        {
          c += (item.Stocks.Price * item.Stocks.NumberOfStocks);
        }
        return c;
      }
    }

    public Company()
    {
      Tickers = new List<Ticker>();
    }

  }
}
