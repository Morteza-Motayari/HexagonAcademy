using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Users.Staffs.Caders
{
    public class UpdateCaderViewModel
    {
        public int Id { get; set; }
        [Display(Name = "دریافتی")]
        public string? Salary { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Position { get; set; }
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید.")]
        public int UserId { get; set; }
        [Display(Name = "مربی")]
        public string? CaderName { get; set; }
        [Display(Name = "نقش")]
        [Required(ErrorMessage = "لطفا حداقل یک {0} را انتخاب کنید.")]
        public List<int> CaderRoleIds { get; set; }
        public bool IsDeleted {  get; set; }
    }
    public enum UpdateCaderResult
    {
        Success,
        CaderNotFound,
        DuplicatedPosition,
        InValidSalary,
        ExistRoleForUser
    }
}
