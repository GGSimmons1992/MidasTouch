using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MidasTouch.Data.Helpers;
using MidasTouch.Domain.Models;

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

        public ActionResult ConfirmUser(User enterred)
        {
            if (enterred.Identity.Email == null)
            {
                return Error("Email is required");
            }

            if (enterred.Identity.Password == null)
            {
                return Error("Password is required");
            }

            var datauser = (new UserHelper()).GetUserByEmail(enterred.Identity.Email);

            if (datauser == null)
            {
                return Error("User is non-existent");
            }

            if (datauser.Identity.Password != enterred.Identity.Password)
            {
                return Error("Passwords do not match");
            }

            HttpContext.Session.SetString("First", enterred.Identity.Name.First);
            HttpContext.Session.SetInt32("userid", enterred.Id);

            return Login();
        }

        public ActionResult Error(string message)
        {
            ViewData["errormessage"] = message;
            return PartialView("_ErrorPartial");
        }


    }
}