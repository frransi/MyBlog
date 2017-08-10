using Microsoft.AspNetCore.Mvc;

namespace blog.ViewComponents
{
    public class NavBarViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
