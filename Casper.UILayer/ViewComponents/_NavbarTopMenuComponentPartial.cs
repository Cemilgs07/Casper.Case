using Microsoft.AspNetCore.Mvc;

namespace Casper.UILayer.ViewComponents
{
    public class _NavbarTopMenuComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
