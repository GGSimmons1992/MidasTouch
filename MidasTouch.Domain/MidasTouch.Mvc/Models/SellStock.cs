using MidasTouch.Data.Helpers;
using MidasTouch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidasTouch.Mvc.Models
{
  public class SellStock
  {
    public double LatestPrice { get; set; }
    public string Symbol { get; set; }
    public int LatestVolume { get; set; }
    public int SellSharesCount { get; set; }
    public User User { get; set; }

    public void Sell()
    {
      var ph = new PortfolioHelper();
      var portfolio = ph.GetPortfolioByUser(User);

      while (SellSharesCount > 0)
      {
        if (SellSharesCount < portfolio.Shares[0].NumberOfShares)
        {
          portfolio.Shares[0].NumberOfShares -= SellSharesCount;
          User.AccountBalance += (portfolio.Shares[0].Price * SellSharesCount);
          SellSharesCount = 0;
        }
        else
        {
          SellSharesCount -= portfolio.Shares[0].NumberOfShares;
          User.AccountBalance += (portfolio.Shares[0].NumberOfShares * portfolio.Shares[0].Price);
          portfolio.Shares.RemoveAt(0);
        }
      }

      ph._db.SaveChanges();
    }
  }
}
