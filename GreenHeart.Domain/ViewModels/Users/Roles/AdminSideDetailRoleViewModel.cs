using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Common;
using GreenHeart.Domain.ViewModels.Users.Staffs.Caders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Users.Roles
{
    public class AdminSideDetailRoleViewModel:BaseAdminDetail
    {
        [Display(Name = "عنوان نقش")]
        public string RoleTitle { get; set; }
        [Display(Name = "دسترسی ها")]
        public ICollection<Permission>? Permissions { get; set; }
        public ICollection<CaderViewModel>? caders { get; set; }
    }
}
