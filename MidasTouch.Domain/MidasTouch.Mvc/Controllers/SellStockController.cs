using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MidasTouch.Data.Helpers;
using MidasTouch.Mvc.Models;

namespace MidasTouch.Mvc.Controllers
{
    public class SellStockController : Controller
    {
    [HttpPost]
    public IActionResult Sell(string symbol, int sellsharescount)
    {
      //Delete the lines below when we have a working login --V
      var testuserlist = (new UserHelper()).GetUsers();
      var mytestuser = testuserlist[0];
      HttpContext.Session.SetString("First", mytestuser.Identity.Name.First);
      HttpContext.Session.SetInt32("userid", mytestuser.Id);
      //Delete the lines above when we have a working login --^

      var SellStock = new SellStock();

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

          if (Stock.LatestVolume > sellsharescount)
          {
            SellStock.SellSharesCount = sellsharescount;
            SellStock.LatestPrice = Stock.LatestPrice;
            var uh = new UserHelper();
            var userlist = uh.GetUsers();
            var userId = HttpContext.Session.GetInt32("userid");
            var myuser = userlist.FirstOrDefault(u => u.Id == userId);
            SellStock.User = myuser;
            SellStock.Symbol = symbol.ToUpper();
            SellStock.Sell();
          }

        }
      }

      return RedirectToAction("DisplayStock", "Stock");
    }
  }
}