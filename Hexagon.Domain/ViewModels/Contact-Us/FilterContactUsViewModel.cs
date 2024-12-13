using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Contact_Us
{
    public class FilterContactUsViewModel:BasePaging<ContactUsViewModel>
    {
        [Display(Name = "تعداد مدل های نمایشی در صفحه")]
        public override int TakeEntity { get; set; }
        [Display(Name = "موضوع")]
        public string? Subject { get; set; }
        [Display(Name = "اسم")]
        public string? FullName { get; set; }
        [Display(Name = "پاسخ دهنده")]
        public string? AnsweredUser { get; set; }
        [Display(Name = "ایمیل")]
        public string? Email { get; set; }
        [Display(Name = "وضعیت موجودیت")]
        public ExistingStatus? Status { get; set; }
        [Display(Name = "وضعیت پاسخ")]
        public FilterContactUsStatus AnswerStatus {  get; set; }
    }
}
