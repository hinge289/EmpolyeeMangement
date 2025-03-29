using EmpolyeeMangement.Models;
using EmpolyeeMangement.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmpolyeeMangement.Controllers
{
    public class AdminController : Controller
    {
        private IAdmin _admin;
        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult AddEmpolyee()
        {
            ViewBag.Designation = new SelectList(_admin.GetDesignation(), "DesignationId", "DesignationName");
            return View();
        }
        [HttpPost]
        public IActionResult AddEmpolyee(Empolyee reg)
        {
            if (string.IsNullOrWhiteSpace(reg.Name))
            {
                ModelState.AddModelError("Name", "Please Enter Name");
            }
            if (reg.File == null)
            {
                ModelState.AddModelError("File", "Choose Image");
            }
            if (reg.Designation == 0)
            {
                ModelState.AddModelError("Designation", "Please Select Designation");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Designation = new SelectList(_admin.GetDesignation(), "DesignationId", "DesignationName");
                return View(reg);
            }
            if (_admin.CheckEmpolyeeExist(reg))
            {
                TempData["Massage"] = "Empolyee Already Exists";
                TempData["MessageType"] = "error";
                ViewBag.Designation = new SelectList(_admin.GetDesignation(), "DesignationId", "DesignationName");

                return View();
            }
            _admin.AddEmpolyee(reg);
            TempData["Message"] = "Empolyee Registartion Successfull";
            TempData["MessageType"] = "success";
            return View();
        }
        public IActionResult EmpolyeeDetails()
        {
            ViewBag.EmpolyeeList = _admin.GetEmpolyeeList();
            return View();
        }
        [HttpPost]
        public IActionResult Login(Empolyee model)
        {
            var user = _admin.checkCreaditioal(model);
            if (user!=null)
            {
                if (user.Password == model.Password)
                {
                    TempData["Role"] = user.Role;
                    return user.Role=="Admin"?RedirectToAction("Dashboard", "Admin"):RedirectToAction("Dashboard","Empolyee");
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
