using Microsoft.EntityFrameworkCore;
using MidasTouch.Data.Helpers;
using MidasTouch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidasTouch.Mvc.Models
{
  public class TradeStock
  {
    public double LatestPrice { get; set; }
    public string CompanyName { get; set; }
    public string Symbol { get; set; }
    public int LatestVolume { get; set; }
    public int TradeSharesCount { get; set; }
    public string State { get; set; }
    public User User { get; set; }

    public bool Trade()
    {
      if (User == null || User.Id <= 0)
      {
        return false;
      }

      var totalCost = (LatestPrice * TradeSharesCount);
      var sh = new ShareHelper();
      var db = sh._db;
      var datauser = db.Users.Where(du => du.Id == User.Id).FirstOrDefault();
      var dataportfolio = db.Portfolios.Include(p => p.Shares).Where(p => p.Id == User.Portfolio.Id).FirstOrDefault();

      if (State == "Buy")
      {
        if (LatestVolume < TradeSharesCount || totalCost > User.AccountBalance)
        {
          return false;
        }
      }

      if (State == "Sell")
      {
        var sl = dataportfolio.Shares.Where(s => s.Symbol == Symbol).ToList();
        var cnt = 0;
        foreach (var item in sl)
        {
          cnt += item.NumberOfShares;
        }

        if (LatestVolume < TradeSharesCount || TradeSharesCount > cnt)
        {
          return false;
        }

        totalCost = -totalCost;
        TradeSharesCount = -TradeSharesCount;
      }

      var share = new Share()
      {
        Symbol = Symbol.ToUpper(),
        NumberOfShares = TradeSharesCount,
        Price = LatestPrice,
        State = State,
        TimeStamp = DateTime.Now
      };

      sh.SetShare(share);
      var datashare = db.Shares.Where(s => s.Symbol == Symbol).LastOrDefault();

      dataportfolio.Shares.Add(datashare);
      datauser.AccountBalance -= totalCost;
      db.SaveChanges();

      return true;
    }
  }
}
