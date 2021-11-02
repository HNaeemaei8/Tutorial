using BarsaTutorial.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BarsaTutorial.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet("login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel request)
        {
            if (request.Username == "برسا" && request.Password == "123")
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,request.Username),
                new Claim(ClaimTypes.Name,request.Username),
                new Claim("RoleId", "Admin")
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var pricipal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true
                };

                HttpContext.SignInAsync(pricipal, properties);

                return Redirect("/");
            }
            return View("Index");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                HttpContext.SignOutAsync();

            return Redirect("/login");
        }
    }
}
