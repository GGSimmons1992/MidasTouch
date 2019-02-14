using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MidasTouch.Data.Helpers;
using MidasTouch.Mvc.Models;

namespace MidasTouch.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            //Delete the lines below when we have a working login --V
            var userlist = (new UserHelper()).GetUsers();
            var myuser = userlist[0];
            HttpContext.Session.SetString("First",myuser.Identity.Name.First);
            HttpContext.Session.SetInt32("userid",myuser.Id);
            //Delete the lines above when we have a working login --^

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
