using Microsoft.AspNetCore.Mvc;

namespace EmpolyeeMangement
{
    public class NavMenuViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(string Role)
        {
            ViewData["Role"] = HttpContext.Session.GetString("Role");
            return View();
        }
    }
}
