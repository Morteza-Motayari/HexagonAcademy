using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Models.Users
{
    public class Permission
    {
        #region Properties
        [Key]
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionTitle { get; set; }
        public int? ParentId { get; set; }
        #endregion

        #region Relations
        public Permission? permission { get; set; }
        public List<RolePermission>? RolePermissions { get; set; }
        #endregion
    }
}
