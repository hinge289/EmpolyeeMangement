using EmpolyeeMangement.Models.Admin;
using EmpolyeeMangement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using EmpolyeeMangement.Models.NewFolder;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace EmpolyeeMangement.Controllers
{
    public class AccountController : Controller
    {

        private IReport _report;

        public AccountController(IReport report)
        {
            _report = report;
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Empolyee model)
        {
            var user = _report.CheckCredentials(model); 
            if (user != null)
            {
                if (user.Password == model.Password)
                {
                    List<Claim> claim = new List<Claim>();
                    claim.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
                    claim.Add(new Claim(ClaimTypes.Role, user.Role));

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    HttpContext.SignInAsync(claimsPrincipal);
                    HttpContext.Session.SetString("Role", user.Role);
                    HttpContext.Session.SetString("EmpId",Convert.ToString(user.EmployeeId));
                    return user.Role == "Admin" ? RedirectToAction("Dashboard", "Admin") : RedirectToAction("Dashboard", "Empolyee");
                }
                else
                {
                    ModelState.AddModelError("Password", "Incurrect Password");
                    return View();
                }
            }
            ModelState.AddModelError("UserName", "Incurrect Email Address");
            return View();

        }
    }
}
