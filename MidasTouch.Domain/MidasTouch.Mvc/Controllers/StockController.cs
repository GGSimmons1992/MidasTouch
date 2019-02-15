using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MidasTouch.Data.Helpers;
using MidasTouch.Mvc.Models;

namespace MidasTouch.Mvc.Controllers
{
  public class StockController : Controller
  {
    public static double StockPrice { get; set; }

    public IActionResult Stock()
    {
      var states = new List<SelectListItem>()
      {
        new SelectListItem { Value = "Buy", Text = "Buy" },
        new SelectListItem { Value = "Sell", Text = "Sell" }
      };
      ViewBag.state = states;
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
          var states = new List<SelectListItem>()
          {
            new SelectListItem { Value = "Buy", Text = "Buy" },
            new SelectListItem { Value = "Sell", Text = "Sell" }
          };
          ViewBag.state = states;
        }
      }
      return View();
    }

    [HttpPost]
    public IActionResult Trade(string symbol, int tradesharescount, string state)
    {
      //Delete the lines below when we have a working login --V
      var testuserlist = (new UserHelper()).GetUsers();
      var mytestuser = testuserlist[0];
      HttpContext.Session.SetString("First", mytestuser.Identity.Name.First);
      HttpContext.Session.SetInt32("userid", mytestuser.Id);
      //Delete the lines above when we have a working login --^

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
          var Stock = response.Content.ReadAsAsync<TradeStock>().GetAwaiter().GetResult();

          var uh = new UserHelper();
          var userlist = uh.GetUsers();
          var userid = HttpContext.Session.GetInt32("userid");
          var myuser = userlist.FirstOrDefault(u => u.Id == userid);

          var TradeStock = new TradeStock()
          {
            User = myuser,
            TradeSharesCount = tradesharescount,
            LatestPrice = Stock.LatestPrice,
            CompanyName = Stock.CompanyName,
            Symbol = symbol.ToUpper(),
            State = state,
            LatestVolume = Stock.LatestVolume
          };

          TradeStock.Trade();
        }
      }
      var states = new List<SelectListItem>()
          {
            new SelectListItem { Value = "Buy", Text = "Buy" },
            new SelectListItem { Value = "Sell", Text = "Sell" }
          };
      ViewBag.state = states;
      return View("Stock");
    }
  }
}
