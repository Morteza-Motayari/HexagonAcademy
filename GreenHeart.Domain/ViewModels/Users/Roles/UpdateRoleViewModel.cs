using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Users.Roles
{
    public class UpdateRoleViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان نقش")] 
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(230, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string RoleTitle { get; set; }
        public List<int>? PermissionsId { get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum UpdateRoleResult
    {
        Success,
        DupliactedRole,
        NotFound
    }
}
