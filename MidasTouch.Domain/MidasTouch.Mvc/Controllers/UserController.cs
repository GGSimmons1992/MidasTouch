using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MidasTouch.Data.Helpers;

namespace MidasTouch.Mvc.Controllers
{
    public class UserController : Controller
    {
        
        public ActionResult Login()
        {
            //Delete the lines below when we have a working login --V
            var testuserlist = (new UserHelper()).GetUsers();
            var mytestuser = testuserlist[0];
            HttpContext.Session.SetString("First", mytestuser.Identity.Name.First);
            HttpContext.Session.SetInt32("userid", mytestuser.Id);
            //Delete the lines above when we have a working login --^
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Signout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }


    }
}