using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MidasTouch.Mvc.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]

        public async Task<IActionResult> OrderHistory()
        {
            //return View(await _db.Portfolio.ToListAsync());
            return View();
        }
    }
}