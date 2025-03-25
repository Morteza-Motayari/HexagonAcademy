using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GreenHeart.MVC.WebExtensions
{
    public static class ModelStateExtension
    {
        public static string GetModelError(this ModelStateDictionary modelState)
        {
            string errorMessage=string.Empty;
            int line = 1;

            foreach(var message in modelState.Values)
            {
                foreach(var error in message.Errors)
                {
                    errorMessage += $"{line}. {error.ErrorMessage}\n";
                    line++;
                }
                
            }
            return errorMessage;
        }
    }
}
