using Microsoft.AspNetCore.Mvc;
using SignalRWithEntityFramework.Repository;

namespace SignalRWithEntityFramework.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository userRepository;

        public AccountController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var userFromDb = await userRepository.GetUserDetails(username, password);

            if (userFromDb == null)
            {
                ModelState.AddModelError("Login", "Invalid credentials");
                return View();
            }

            HttpContext.Session.SetString("Username", userFromDb.Username);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction(nameof(SignIn));
        }
    }
}
