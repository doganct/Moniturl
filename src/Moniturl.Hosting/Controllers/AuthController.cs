using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moniturl.Core;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Moniturl.Hosting.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            this._mapper = mapper;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginDto = _mapper.Map<LoginDto>(model);
            var serviceResult = await _userService.LoginAsync(loginDto);

            if (!serviceResult.Success)
            {
                serviceResult.ErrorMessages.ForAll(error => ModelState.AddModelError(error.Key, error.Value));
                return View(model);
            }

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                      new ClaimsPrincipal(CookieAuthenticationExtensions.SignIn(serviceResult.Result)));


            return Redirect("/");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registerDto = _mapper.Map<RegisterDto>(model);
            var result = await _userService.RegisterAsync(registerDto);

            if (!result.Success)
            {
                result.ErrorMessages.ForAll(error => ModelState.AddModelError(error.Key, error.Value));
                return View(model);
            }

            return RedirectToAction(nameof(Login));
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
