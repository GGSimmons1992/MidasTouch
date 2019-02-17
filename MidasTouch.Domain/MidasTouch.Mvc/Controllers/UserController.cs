using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MidasTouch.Data.Helpers;
using MidasTouch.Domain.Models;
using MidasTouch.Mvc.Models;

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
            var datauser = (new UserHelper()).GetUserByEmail(enterred.Identity.Email);

            if (datauser == null)
            {
                ViewData["message"] = "User is non-existent";
                return View("Login");
            }

            if (datauser.Identity.Password != enterred.Identity.Password)
            {
                ViewData["message"] = "Passwords do not match";
                return View("Login");
            }

            HttpContext.Session.SetString("First", datauser.Identity.Name.First);
            HttpContext.Session.SetInt32("userid", datauser.Id);

            ViewData["message"] = $"Login Succeded: Welcome {datauser.Identity.Name.First} {datauser.Identity.Name.Last}";
            return View("Login");
        }

        public ActionResult Account()
        {
            ViewData["message"] = $"Not implemented yet. Come back soon!";
            return View("Login");
        }

        public ActionResult ValidateRegistration(NewUserModel newuser)
        {

            var datauser = (new UserHelper()).GetUserByEmail(newuser.Identity.Email);

            if (datauser != null)
            {
                ViewData["msg"] = $"{newuser.Identity.Email} exists in our database. Please choose a different email";
                return View("Register");
            }

            if (newuser.Secondary!=newuser.Identity.Password)
            {
                ViewData["msg"] = "Passwords do not match.";
                return View("Register");
            }

            var gooduser = new User()
            {
                AccountBalance = 2000,
                Identity = new Identity()
                {
                    Name = new Name()
                    {
                        First = newuser.Identity.Name.First,
                        Last = newuser.Identity.Name.Last,
                    },
                    Email = newuser.Identity.Email,
                    Password = newuser.Identity.Password
                }
            };
            (new UserHelper()).SetUser(gooduser);

            ViewData["msg"] = $"Welcome {newuser.Identity.Name.First} {newuser.Identity.Name.Last}, please log in with credentials to continue";
            return View("Register");
        }

    }
}