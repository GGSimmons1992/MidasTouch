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



        public void Buy(string symbol, int buysharescount,Portfolio p)
        {
            var share = new Share();

            share.Symbol = symbol;
            share.NumberOfShares = buysharescount;
            share.Price = LatestPrice;

            var sh = new ShareHelper();
            sh.SetShare(share);
            var datashare=sh._db.Shares.Where(s => s.Symbol == symbol).LastOrDefault();
            p.Shares.Add(datashare);
            sh._db.SaveChanges();

        }
    }

}
