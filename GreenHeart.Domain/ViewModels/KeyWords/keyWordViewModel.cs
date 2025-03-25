using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.KeyWords
{
    public class keyWordViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عبارت")]
        public string Key { get; set; }
        public int SportClassId { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsDeleted { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
    }
}
