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
            var testuserlist = (new UserHelper()).GetUsers();
            var mytestuser = testuserlist.FirstOrDefault(u=>u.Id==HttpContext.Session.GetInt32("userid"));


            var portfolioHelper = new PortfolioHelper();
            var UserPortfolio = portfolioHelper.GetPortfolioByUser(mytestuser).Shares.ToList();


            //var Portfolio = new PortfolioViewModel();

            return View("OrderHistory", UserPortfolio);
        }
    }
}