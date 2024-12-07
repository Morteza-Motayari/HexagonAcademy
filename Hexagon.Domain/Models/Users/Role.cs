using Hexagon.Domain.Models.Common;
using Hexagon.Domain.Models.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Models.Users
{
    public class Role:BaseEntity<int>
    {
        #region Properties
        public string RoleTitle { get; set; }
        #endregion

        #region Relations
        public List<UserRole>? userRoles { get; set; }
        public List<RolePermission>? RolePermissions { get; set; }

        #endregion
    }
}
