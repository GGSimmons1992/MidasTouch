using Microsoft.EntityFrameworkCore;
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
      var db = ph._db;
     

      var datauser = db.Users.Where(du => du.Id == User.Id).FirstOrDefault();
      var dataportfolio = db.Portfolios.Include(x=>x.Shares).Where(dp => dp.Id == User.Portfolio.Id).FirstOrDefault();
        
      var i = 0;
      while (SellSharesCount > 0)
      {
        if (SellSharesCount < dataportfolio.Shares[i].NumberOfShares)
        {
          dataportfolio.Shares[i].NumberOfShares -= SellSharesCount;
          datauser.AccountBalance += (dataportfolio.Shares[i].Price * SellSharesCount);
          SellSharesCount = 0;
        }
        else
        {
          SellSharesCount -= dataportfolio.Shares[i].NumberOfShares;
          datauser.AccountBalance += (dataportfolio.Shares[i].NumberOfShares * dataportfolio.Shares[i].Price);
          dataportfolio.Shares[i].NumberOfShares = 0;
          i++;
          

          
        }
      }

      db.SaveChanges();
    }
  }
}
