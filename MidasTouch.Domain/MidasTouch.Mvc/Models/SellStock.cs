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
      var shares = ;

      while (SellSharesCount > 0)
      {
        if (SellSharesCount < shares[0].NumberOfShares)
        {
          shares[0].NumberOfShares -= shares;
          User.AccountBalance += (shares[0].Price * SellSharesCount);
          SellSharesCount = 0;
        }
        else
        {
          SellSharesCount -= shares[0].NumberOfShares;
          User.AccountBalance += (shares[0].NumberOfShares * shares[0].Price);
          User.Portfolio.Shares.Remove(shares[0]);
          shares.RemoveAt(0);
        }
      }
    }
  }
}
