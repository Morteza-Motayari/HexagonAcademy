using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Models.Users
{
    public class RolePermission
    {
        #region Properties
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(RoleId))]
        public Role role { get; set; }
        [ForeignKey(nameof (PermissionId))]
        public Permission permission { get; set; }
        #endregion
    }
}
