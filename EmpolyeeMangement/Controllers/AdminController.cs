using System.Data;
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
            ViewBag.Designation = new SelectList(_admin.GetDesignation(), "DesignationId", "DesignationName");

            return View();
        }
        public IActionResult Import()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Import(IFormFile file)
        {
            if (file == null)
            {
                ViewBag.Message = "Please select a file";
                return View("Import");
            }
            string extention = Path.GetExtension(file.FileName).ToLower();
            DataTable dataTable = new DataTable();
            if (extention == ".csv")
            {
                dataTable = _admin.ReadCsvFile(file);
            }
            else if (extention == ".xls" || extention == ".xlsx")
            {
                dataTable = _admin.ReadExcelFile(file);
            }
            else
            {
                ViewBag.Message = " Unsupported file type.";
                return View("Import");
            }
            if (dataTable != null)
            {
                var check = _admin.UplodeScanDocument(dataTable);
                if (check)
                {
                    ViewBag.Message = "File Imported Successfully.";
                }
                else
                {
                    ViewBag.Message = " Incorrect file format";
                }

            }
            else
            {
                ViewBag.Message = $"Uploded File is null";
            }
            return View("Import");

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
                    HttpContext.Session.SetString("Role", user.Role);
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
        public IActionResult Attendance()
        {
            ViewBag.EmpolyeeList= _admin.GetEmpolyeeList();
            return View();
        }
          [HttpPost]
        public IActionResult SetAttendance(List<Attendance> EmpAtt, int Month, int Year)
        {
            if (EmpAtt == null || !EmpAtt.Any())
            {
                ModelState.AddModelError("", "No attendance data received.");
                return View();
            }

            // Example: Save the attendance records to the database
            foreach (var attendance in EmpAtt)
            {
                // Set the Month and Year for each attendance record
                attendance.Month = Month;
                attendance.Year = Year;

                // Save the attendance data to your database
               // _admin.SaveAttendance(attendance); // This is a pseudo method. Replace it with your actual data-saving logic
            }

            return RedirectToAction("Success");
        }

    }
}
