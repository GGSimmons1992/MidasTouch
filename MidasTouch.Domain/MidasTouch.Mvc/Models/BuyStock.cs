using MidasTouch.Data.Helpers;
using MidasTouch.Domain.Models;
using System.Linq;

namespace MidasTouch.Mvc.Models
{
    public class BuyStock
    {
        public double LatestPrice { get; set; }
        public string CompanyName { get; set; }
        public double MarketCap { get; set; }
        public string Symbol { get; set; }
        public int LatestVolume { get; set; }
        public int BuySharesCount { get; set; }



        public bool Buy(string symbol, int buysharescount,User u)
        {

            var share = new Share();

            share.Symbol = symbol;
            share.NumberOfShares = buysharescount;
            share.Price = LatestPrice;
            var totalCost = (share.Price * share.NumberOfShares);

            if (u == null)
            {
                return false;
            }

            if (u.Id<=0)
            {
                return false;
            }

            if (totalCost > u.AccountBalance)
            {
                return false;
            }

            var sh = new ShareHelper();
            var db = sh._db;
            var datauser = db.Users.Where(du=>du.Id==u.Id).FirstOrDefault();
            var dataportfolio = db.Portfolios.Where(p => p.Id == u.Portfolio.Id).FirstOrDefault();

            sh.SetShare(share);
            var datashare=db.Shares.Where(s => s.Symbol == symbol).LastOrDefault();
            
            dataportfolio.Shares.Add(datashare);
            datauser.AccountBalance -= totalCost;
            db.SaveChanges();

            return true;
        }
    }

}
