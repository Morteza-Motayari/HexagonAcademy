using Hexagon.Application.Services.Implementation.Gyms;
using Hexagon.Application.Services.Interfaces.Banners;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Shared;
using Hexagon.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hexagon.MVC.Controllers
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
