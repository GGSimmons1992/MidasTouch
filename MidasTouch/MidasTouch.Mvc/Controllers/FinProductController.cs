using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MidasTouch.Mvc.Controllers
{
    public class FinProductController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}