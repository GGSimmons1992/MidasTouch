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

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Signout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
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

            return RedirectToAction("Index", "Home");
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

            if (newuser.Secondary != newuser.Identity.Password)
            {
                ViewData["msg"] = "Passwords do not match.";
                return View("Register");
            }

            var gooduser = new User()
            {
                AccountBalance = 500000,
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

            return RedirectToAction("Login");           
        }

    }
}