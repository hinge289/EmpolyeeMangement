using Microsoft.AspNetCore.Mvc;

namespace EmpolyeeMangement.Controllers
{
    public class TempController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
