using GreenHeart.Application.Services.Implementation.Gyms;
using GreenHeart.Application.Services.Interfaces.Users;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Users.Staffs.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Controllers
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
        #endregion

        #region Detail
        [HttpGet("/Trainers/{Slug}")]
        public async Task<IActionResult> Detail(string Slug)
        {
            var trainer = await staffService.GetTrainerDetailAsync(Slug);
            if (trainer == null)
                return NotFound();
            ViewData["Title"] = Titles.TrinerDetail + trainer.FullName;
            return View(trainer);
        }
        #endregion


    }
}
