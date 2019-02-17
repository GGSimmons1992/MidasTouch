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
            

            var portfolioHelper = new PortfolioHelper();
            var UserPortfolio = portfolioHelper.GetPortfolioByUser(myuser).Shares.ToList();


            //var Portfolio = new PortfolioViewModel();

            return View("OrderHistory", UserPortfolio);
        }
    }
}