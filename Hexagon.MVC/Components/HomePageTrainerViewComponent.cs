using Hexagon.Application.Services.Implementation.Users;
using Hexagon.Application.Services.Implementation.Wallets;
using Hexagon.Application.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Components
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
