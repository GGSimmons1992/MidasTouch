namespace MidasTouch.Mvc.Models
{
    public class StockQuote
    {

        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public string Sector { get; set; }
        public double LatestPrice { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public string LatestTime { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double MarketCap { get; set; }
        public double ChangePercent { get; set; }
        public double Week52High { get; set; }
        public double Week52Low { get; set; }
        public int ShareCount { get; set; }

    }
}
