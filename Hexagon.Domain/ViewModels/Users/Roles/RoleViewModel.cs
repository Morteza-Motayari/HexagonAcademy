using Hexagon.Domain.Models.Links;
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
        public string RoleTitle { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<UserRole>? userRoles { get; set; }

    }
}
