using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MidasTouch.Data;
using MidasTouch.Data.Helpers;

namespace MidasTouch.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly MidasTouchDBContext _context = new MidasTouchDBContext();

        [HttpGet]

        //public async Task<IActionResult> OrderHistory()
        //{
        //    return View(await _context.Shares.ToListAsync());

        //}

        public IActionResult Orders()
        {
            if (HttpContext.Session.GetInt32("userid") == null)
            {
                ViewData["message"] = "Please login";
                return View("Login", "User");
            }

            var userlist = (new UserHelper()).GetUsers();
            var myuser = userlist.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("userid"));


            ViewData["AccountBalance"] = myuser.AccountBalance.ToString("#,##0");

            var portfolioHelper = new PortfolioHelper();
            var PortfolioValue = portfolioHelper.GetPortfolioByUser(myuser);
            ViewData["PortfolioValue"] = PortfolioValue.Value.ToString("#,##0");
            var UserPortfolio = portfolioHelper.GetPortfolioByUser(myuser).Shares.ToList();

            return View("OrderHistory", UserPortfolio);
        }
    }
}