using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Models.Users
{
    public class UserRole
    {
        #region Properties
        [Key]
        public int UserRoleId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User user { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role role { get; set; }
        #endregion
    }
}
