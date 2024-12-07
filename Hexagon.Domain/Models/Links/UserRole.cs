using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagon.Domain.Models.Users;

namespace Hexagon.Domain.Models.Links
{
    public class UserRole
    {
        #region Properties
        [Key]
        public int UserRoleId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public int? CaderId {  get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User user { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role role { get; set; }
        [ForeignKey(nameof(CaderId))]
        public Staff? Cader { get; set; }
        #endregion
    }
}
