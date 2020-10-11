using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moniturl.Core;
using System.Threading.Tasks;

namespace Moniturl.Hosting.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            this._userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return RedirectToAction(nameof(Login));
        }


    }
}
