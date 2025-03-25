using GreenHeart.Domain.Enums.Tickets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Tickets.Tickets
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        [Display(Name = "سازنده")]
        public string CreatorName { get; set; }
        [Display(Name ="عنوان")]
        public string Title { get; set; }
        [Display(Name ="وضعیت")]
        public TicketStatus Status { get; set; }
        [Display(Name ="بخش")]
        public TicketSection Section { get; set; }
        [Display(Name ="اولویت")]
        public TicketPriority Priority { get; set; }
        [Display(Name ="تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
