using EmpolyeeMangement.Models.Admin;
using EmpolyeeMangement.Models.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmpolyeeMangement.Controllers
{
    [Authorize(Roles ="Employee")]
    public class EmpolyeeController : Controller
    {
        private IEmployee _employee;

        public EmpolyeeController(IEmployee employee)
        {
            _employee = employee;
        }

  
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        { 
            
            return View();
        }
        public IActionResult GenrateSalary()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GenrateSalary(int Month,int Year)
        {
            int EmployeeId = Convert.ToInt32(HttpContext.Session.GetString("EmpId"));
            var spResult = _employee.GetSalaryRecord(Month, Year, EmployeeId);

            if (spResult != null)
            {
                var dt = _employee.GetDataIntoDT(spResult);
                var check = _employee.SendSalarySlipToEmpolyee(dt);
                if (check)
                {
                    TempData["Message"] = "Empolyee Salary Slip Send Successfully";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Massage"] = "Something Wents Wrong";
                    TempData["MessageType"] = "error";
                }
                return View();
            }
            else
            {
                TempData["Message"] = "Data not found";
                TempData["MessageType"] = "error";
            }
            return View();
           
        }
    }
}
