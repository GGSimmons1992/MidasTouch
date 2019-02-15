using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MidasTouch.Mvc.Models;

namespace MidasTouch.Mvc.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            var IEXTrading_API_PATH = "https://api.iextrading.com/1.0/stock/market/news/last/8";

            var News = new HomePageMarketNews();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //For IP-API
                client.BaseAddress = new Uri(IEXTrading_API_PATH);
                HttpResponseMessage response = client.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {

                    var companyNewsList = response.Content.ReadAsAsync<List<HomePageMarketNews>>().GetAwaiter().GetResult();

                    ViewData["MarketNews"] = companyNewsList;

                    News.MarketNews = companyNewsList;

                }



            }
            return View("Index", News);
        }
    }
}

