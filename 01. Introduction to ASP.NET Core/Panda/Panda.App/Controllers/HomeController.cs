namespace Panda.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
                return this.View("IndexUser");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                return this.View("IndexAdmin");
            }
            else
            {
                return this.View();
            }
        }
    }
}
