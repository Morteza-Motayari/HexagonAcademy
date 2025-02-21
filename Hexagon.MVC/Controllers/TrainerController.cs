using Hexagon.Application.Services.Implementation.Gyms;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Users.Staffs.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Controllers
{
    public class TrainerController(IStaffService staffService) : BaseSiteController
    {
        #region List
        [Route("/Trainers")]
        public async Task<IActionResult> List(ClientSideFilterTrainerViewModel filter)
        {            
            var trainers=await staffService.ClientSideFilterTrainerAsync(filter);
            ViewData["Title"] = Titles.Triners; 
            return View(trainers);
        }
        [HttpGet("/Trainers/{Slug}")]
        public async Task<IActionResult> Detail(string Slug)
        {
            var trainer = await staffService.GetTrainerDetailAsync(Slug);
            if (trainer == null)
                return NotFound();
            ViewData["Title"] = Titles.TrinerDetail+trainer.FullName;
            return View(trainer);
        }
        #endregion
    }
}
