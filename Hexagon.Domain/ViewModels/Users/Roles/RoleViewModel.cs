using Hexagon.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Users.Roles
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان نقش")] 
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(230, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string RoleTitle { get; set; }
        [Display(Name = " اسم نقش (انگلیسی) ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(230, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string RoleName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<UserRole>? userRoles { get; set; }

    }
}
