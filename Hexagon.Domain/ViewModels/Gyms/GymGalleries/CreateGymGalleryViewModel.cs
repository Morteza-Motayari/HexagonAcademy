using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Gyms.GymGalleries
{
    public class CreateGymGalleryViewModel
    {
        public int GymId { get; set; }
        [Display(Name = "عنوان عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public IFormFile? Image { get; set; }
        [Display(Name = "عنوان عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string ImageTitle { get; set; }
    }
    public enum CreateGymGalleryResult
    {
        Success
    }
}
