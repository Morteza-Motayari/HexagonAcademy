using GreenHeart.Domain.Enums.Tickets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Tickets.TicketMessages
{
    public class ClientSideCreateTicketMessageViewModel
    {
        [Display(Name ="عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200,ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Title { get; set; }
        [Display(Name = "پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(2000, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Message { get; set; }
        [Display(Name = "بخش")]
        public TicketSection Section { get; set; }
        [Display(Name = "اولویت")]
        public TicketPriority Priority { get; set; }
    }
    public enum ClientSideCreateTicketMessageResult
    {
        Success
    }
}
