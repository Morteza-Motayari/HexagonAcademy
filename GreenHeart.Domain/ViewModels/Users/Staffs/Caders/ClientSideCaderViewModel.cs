using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Users.Staffs.Caders
{
    public class ClientSideCaderViewModel
    {
        [Display(Name = "نام کامل")]
        public string FullName { get; set; }
        [Display(Name = "ایمیل")]
        public string email { get; set; }
        [Display(Name = "عنوان")]
        public string Position { get; set; }
        public string Avatar { get; set; }
    }
}
