using Microsoft.AspNetCore.Mvc;

namespace SignalRWithEntityFramework.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(string username, string password)
        {

        }
    }
}
