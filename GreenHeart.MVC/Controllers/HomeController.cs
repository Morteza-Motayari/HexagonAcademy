using GreenHeart.Application.Services.Implementation.Gyms;
using GreenHeart.Application.Services.Interfaces.Banners;
using GreenHeart.Application.Services.Interfaces.Gyms;
using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Shared;
using GreenHeart.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GreenHeart.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBannerService _bannerService;
        private readonly ISportClassService _sportClassService;

        public HomeController(ILogger<HomeController> logger, IBannerService bannerService,ISportClassService sportClassService)
        {
            _logger = logger;
            _bannerService = bannerService;
            _sportClassService = sportClassService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["banner"]=await _bannerService.ClientSideBannerViewModel();
            var sportClasses = await _sportClassService.GetClassesForIndexPage(FilterUserGender.All);
            ViewData["Title"] = Titles.Home;
            return View(sportClasses);
        }
        [HttpGet]
        public async Task<IActionResult> GetClasses(FilterUserGender filter)
        {
            var sportClasses = await _sportClassService.GetClassesForIndexPage(filter);
            return PartialView("_SportClasses",sportClasses);
            
        }

    }
}
