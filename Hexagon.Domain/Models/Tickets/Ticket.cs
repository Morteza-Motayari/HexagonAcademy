using Hexagon.Domain.Enums.Tickets;
using Hexagon.Domain.Models.Common;
using Hexagon.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Models.Tickets
{
    public class Ticket:BaseEntity<int>
    {
        #region Properties
        public string Title { get; set; }
        public TicketStatus Status { get; set; }
        public TicketSection Section { get; set; }
        public TicketPriority Priority { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(CreatedBy))]
        public User User { get; set; }
        public ICollection<TicketMessage> TicketMessages { get; set; }
        #endregion
    }
}
