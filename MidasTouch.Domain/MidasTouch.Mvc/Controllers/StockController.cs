using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MidasTouch.Data.Helpers;
using MidasTouch.Mvc.Models;

namespace MidasTouch.Mvc.Controllers
{
    public class StockController : Controller
    {
        public static double StockPrice { get; set; }

        public IActionResult Stock()
        {
            return View("Stock");
        }

        [HttpPost]
        public IActionResult Stock(string symbol)
        {
            var IEXTrading_API_PATH = "https://api.iextrading.com/1.0/stock/{0}/quote";

            IEXTrading_API_PATH = string.Format(IEXTrading_API_PATH, symbol);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //For IP-API
                client.BaseAddress = new Uri(IEXTrading_API_PATH);
                HttpResponseMessage response = client.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var Stock = response.Content.ReadAsAsync<StockQuote>().GetAwaiter().GetResult();

                    string MarketCapFormatted = Stock.MarketCap.ToString("#,##0");


                    ViewBag.Symbol = Stock.Symbol;
                    ViewBag.CompanyName = Stock.CompanyName;
                    ViewBag.Price = Stock.LatestPrice;
                    StockPrice = Stock.LatestPrice;
                    ViewBag.MarketCap = MarketCapFormatted;
                    ViewBag.PercentChange = Stock.ChangePercent;
                }


            }

            return View();
        }



        [HttpPost]
        public IActionResult Buy(string symbol, int buysharescount)
        {
            var BuyStock = new BuyStock();

            var IEXTrading_API_PATH = "https://api.iextrading.com/1.0/stock/{0}/quote";

            IEXTrading_API_PATH = string.Format(IEXTrading_API_PATH, symbol);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //For IP-API
                client.BaseAddress = new Uri(IEXTrading_API_PATH);
                HttpResponseMessage response = client.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var Stock = response.Content.ReadAsAsync<BuyStock>().GetAwaiter().GetResult();

                    string MarketCapFormatted = Stock.MarketCap.ToString("#,##0");
                    
                    

                    if (Stock.LatestVolume > buysharescount)
                    {
                        BuyStock.BuySharesCount = buysharescount;

                        BuyStock.LatestPrice = Stock.LatestPrice;
                        BuyStock.CompanyName = Stock.CompanyName;

                        var uh = new UserHelper();
                        var userlist = uh.GetUsers();
                        var userid = HttpContext.Session.GetInt32("userid");

                        var myuser = userlist.FirstOrDefault(u => u.Id == userid);

                        BuyStock.Buy(symbol, buysharescount, myuser);
                    }

                }
            }

            return View("Stock");
        }


    }
}
