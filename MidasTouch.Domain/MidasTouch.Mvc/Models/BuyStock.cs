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

            if (totalCost > u.AccountBalance)
            {
                return false;
            }

            if (u.Id<=0 || u==null)
            {
                return false;
            }

            var sh = new ShareHelper();
            var db = sh._db;
            sh.SetShare(share);
            var datashare=db.Shares.Where(s => s.Symbol == symbol).LastOrDefault();
            u.AccountBalance -= totalCost;
            u.Portfolio.Shares.Add(datashare);
            db.SaveChanges();

            return true;
        }
    }

}
