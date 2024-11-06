using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Hexagon.Domain.ViewModels.Gyms.GymGalleries
{
    public class GymGalleryViewModel
    {
        public int Id { get; set; }
        public int GymId { get; set; }
        [Display(Name = "عنوان عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string ImageUrl { get; set; }
        [Display(Name = "عنوان عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string ImageTitle { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
    }
}
