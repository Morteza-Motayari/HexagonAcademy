using GreenHeart.Domain.Models.Common;
using GreenHeart.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Models.Tickets
{
    public class TicketMessage:BaseEntity<int>
    {
        #region Properties
        public int TicketId { get; set; }
        public string Message { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(TicketId))]
        public Ticket Ticket { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        public User Sender { get; set; }
        #endregion
    }
}
