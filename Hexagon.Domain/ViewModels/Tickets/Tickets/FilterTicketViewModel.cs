using Hexagon.Domain.Enums.Tickets;
using Hexagon.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Tickets.Tickets
{
    public class FilterTicketViewModel:BasePaging<TicketViewModel>
    {
        [Display(Name = "سازنده")]
        public int? CreatorId { get; set; }
        public string? CreatorName { get; set; }
        [Display(Name = "عنوان")]
        public string? Title { get; set; }
        [Display(Name = "وضعیت")]
        public FilterTicketStatus? Status { get; set; }
        [Display(Name = "بخش")]
        public FilterTicketSection? Section { get; set; }
        [Display(Name = "اولویت")]
        public FilterTicketPriority? Priority { get; set; }
    }
}
