using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.ClassComments
{
    public class ClientSideUpdateCommentViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(150, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Comment { get; set; }
        public int ClassId {  get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum ClientSideUpdateCommentResult
    {
        Success,
        ClassCommentNotFound
    }
}
