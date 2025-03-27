using EmpolyeeMangement.Models;
using EmpolyeeMangement.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

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
        public IActionResult Dashboard  ()
        {
            return View();
        }
        public IActionResult AddEmpolyee()
        {
            var list = _admin.GetDesignation();
            ViewBag.Designation = list;
            //    return View(new Registration());
            return View();
        }
        [HttpPost]
        public IActionResult AddEmpolyee(Registration reg)
        {
            if (string.IsNullOrWhiteSpace(reg.Name))
            {
                ModelState.AddModelError("Name", "Please Enter Name");
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
            return View();
        }
        public IActionResult EmpolyeeDetails()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login model)
        {

            return RedirectToAction("Dashboard","Admin");
        }
    }
}
