using Microsoft.AspNetCore.Mvc;

namespace EmpolyeeMangement
{
    public class NavMenuViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(string Role)
        {
            ViewData["Role"] = Role;
            return View();
        }
    }
}
