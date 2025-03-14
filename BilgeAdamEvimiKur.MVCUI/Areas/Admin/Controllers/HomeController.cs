using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeAdamEvimiKur.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        readonly IAppUserManager _userManager;

        public HomeController(IAppUserManager appUserManager)
        {
            _userManager = appUserManager;
        }

        public IActionResult Index()
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            return View();
        }

        

    }
}
