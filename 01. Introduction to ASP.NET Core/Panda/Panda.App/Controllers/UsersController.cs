namespace Panda.App.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Panda.Domain.Models;
    using Panda.Services.Interfaces;
    using Panda.Web.ViewModels.Users;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly SignInManager<PandaUser> signInManager;
        private readonly UserManager<PandaUser> userManager;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Login()
        {
            return this.View();
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect("/Users/Register");
            }

            this.usersService.CreateUser(input.Username, input.Email, input.Password);

            return this.Redirect("/Users/Login");
        }

        [HttpPost]
        public IActionResult Login(LoginInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect("/Users/Login");
            }



            return this.Redirect("/Home/Index");
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return this.Redirect("/Home/Index");
        }
    }
}
