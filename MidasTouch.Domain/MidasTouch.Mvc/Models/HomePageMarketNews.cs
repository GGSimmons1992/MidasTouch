using System;
using System.Collections.Generic;

namespace MidasTouch.Mvc.Models
{
    public class HomePageMarketNews
    {
        public DateTime Datetime { get; set; }
        public string Headline { get; set; }
        public string Source { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public string Related { get; set; }

        public List<HomePageMarketNews> MarketNews { get; set; }
    }
}
