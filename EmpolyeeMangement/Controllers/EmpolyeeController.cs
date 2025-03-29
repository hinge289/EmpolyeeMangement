using Microsoft.AspNetCore.Mvc;

namespace EmpolyeeMangement.Controllers
{
    public class EmpolyeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        { 
            
            return View();
        }
      
    }
}
