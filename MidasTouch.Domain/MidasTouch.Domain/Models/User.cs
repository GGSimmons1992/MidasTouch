﻿using MidasTouch.Domain.Abstracts;
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
      var c = Market.Companies.FirstOrDefault(s => s.Name == name);
      if (c == null)
      {
        return false;
      }

      var t = c.Tickers.FirstOrDefault(s => s.Symbol == ticker);
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
      AccountBalance -= (sh.Price * shares);
      Portfolio.Shares.Add(sh);
      return true;
    }

    public bool Sell(string name, string ticker, int shares)
    {
      var c = Market.Companies.FirstOrDefault(s => s.Name == name);
      if (c == null)
      {
        return false;
      }

      var t = c.Tickers.FirstOrDefault(s => s.Symbol == ticker);
      if (t == null)
      {
        return false;
      }

      var st = Portfolio.Shares.FirstOrDefault(s => s.Symbol == ticker);
      if (st == null)
      {
          return false;
      }

      var sh = Portfolio.Shares.Where(s => s.Symbol == ticker).ToList();
      var am = 0;
      foreach (var item in sh)
      {
        am += item.NumberOfShares;
      }

      if (am < shares)
      {
          return false;
      }

      while (shares > 0)
      {
        if (shares < sh[0].NumberOfShares)
        {
          sh[0].NumberOfShares -= shares;
          AccountBalance += (sh[0].Price * shares);
          t.Stocks.NumberOfStocks += shares;
          shares = 0;
        }
        else
        {
          shares -= sh[0].NumberOfShares;
          t.Stocks.NumberOfStocks += sh[0].NumberOfShares;
          AccountBalance += (sh[0].NumberOfShares * sh[0].Price);
          Portfolio.Shares.Remove(sh[0]);
          sh.RemoveAt(0);
        }
      }

      return true;
    }

    public override bool IsValid()
    {
      return
        Portfolio.IsValid() &&
        Identity.IsValid() &&
        Validator.ValidateMoney(this);
    }
  }
}
