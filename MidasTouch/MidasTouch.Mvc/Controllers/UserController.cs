using Microsoft.AspNetCore.Mvc;

namespace MidasTouch.Mvc.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Buy()
        {
            return View();
        }
    }
}