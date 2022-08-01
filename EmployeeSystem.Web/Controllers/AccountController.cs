using EmployeeSystem.Models.ViewModels;
using EmployeeSystem.Web.Abstraction;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountHelper accountHelper;
        public AccountController(IAccountHelper accountHelper)
        {
            this.accountHelper = accountHelper;
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = accountHelper.Login(model);
                if (!string.IsNullOrEmpty(token.Token))
                {
                    var claims = new List<Claim>() {
                    new Claim("Token", token.Token),
                    new Claim(ClaimTypes.Name, token.Name)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });

                    HttpContext.Session.SetString("Token", token.Token);
                    HttpContext.Session.SetString("Name", token.Name);
                    return RedirectToAction("Index", "Employee");
                }

                ModelState.AddModelError("", "Invalid Credentials");
            }

            ModelState.AddModelError("", "Provide valid credentials");

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = accountHelper.Register(model);
                return RedirectToAction(nameof(Login));
            }


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
