using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.AppUserDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.ProfileDTOs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppProfileVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ErrorVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace BilgeAdamEvimiKur.MVCUI.Controllers
{
    public class HomeController : Controller
    {

      

        public HomeController()
        {

        }

        public IActionResult Index()
        {
            
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            return View();

        }

        [Authorize(Roles = "Member")]
        public IActionResult Privacy()
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            TempData["Result"] = "Bu sayfa sadece 'Member' rol�ne sahip �yelere �zeldir. Sadece bu �yeler sayfay� g�r�nt�leyebilir.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult RedirectPanel()
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            return View();
        }
    }
}