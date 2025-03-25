using GreenHeart.Domain.Models.Common;
using GreenHeart.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Models.Contact_Us
{
    public class ContactUs:BaseEntity<int>
    {
        #region Properties
        public string Subject { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; } 
        public string Description { get; set; }
        public bool IsAnswered { get; set; }
        public string? Answer { get; set; }
        public string IP { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(LastModifiedBy))]
        public User? AnsweredUser { get; set; }
        #endregion
    }
}
