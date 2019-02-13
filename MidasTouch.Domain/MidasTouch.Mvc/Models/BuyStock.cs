using MidasTouch.Domain.Models;

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



        public void Buy(string symbol, int buysharescount)
        {
            var share = new Share();

            share.Symbol = symbol;
            share.NumberOfShares = buysharescount;

        }
    }

}
