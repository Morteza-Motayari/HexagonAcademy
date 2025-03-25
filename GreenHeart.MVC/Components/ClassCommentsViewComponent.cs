using GreenHeart.Application.Services.Interfaces.Gyms;
using GreenHeart.Domain.Interfaces.Gyms;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Components
{
    public class ClassCommentsViewComponent(IClassCommentService classCommentService): ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int sportClassId)
        {
            var classComments=await classCommentService.GetClientClassActiveCommentAsync(sportClassId);
            return View("ClassComments",classComments);
        }
    }
}
