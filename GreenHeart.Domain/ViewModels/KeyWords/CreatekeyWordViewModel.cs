using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.KeyWords
{
    public class CreatekeyWordViewModel
    {
        [Display(Name ="عبارت")] 
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(30, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Key { get; set; }
        public int SportClassId { get; set; }
    }
    public enum CreatekeyWordResult
    {
        Success,
        KeyDuplicated
    }
}
