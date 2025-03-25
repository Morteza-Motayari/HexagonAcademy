using GreenHeart.Application.Services.Implementation.Users;
using GreenHeart.Application.Services.Implementation.Wallets;
using GreenHeart.Application.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Components
{
    public class HomePageTrainerViewComponent(IStaffService staffService): ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Trainers = await staffService.GetTrainersForHomePageAsync();
            return View("HomePageTrainer", Trainers);
        }
    }
}
