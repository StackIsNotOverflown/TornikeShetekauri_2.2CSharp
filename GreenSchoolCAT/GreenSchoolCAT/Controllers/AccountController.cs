using GreenSchoolCAT.Data;
using GreenSchoolCAT.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GreenSchoolCAT.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;

        public AccountController(IConfiguration config)
        {
            _userRepository = new UserRepository(config.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> LogoutSilent()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string fullName, string password)
        {
            var user = _userRepository.DecryptedUser(fullName);

            if (user == null)
            {
                ViewBag.ErrorMessage = "არასწორი სახელი ან პაროლი";
                return View();
            }

            if (user.Password != password)
            {
                ViewBag.ErrorMessage = "არასწორი სახელი ან პაროლი";
                return View();
            }
 

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return user.Role == "Teacher"
                ? RedirectToAction("TeacherHome", "Home")
                : RedirectToAction("StudentHome", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
