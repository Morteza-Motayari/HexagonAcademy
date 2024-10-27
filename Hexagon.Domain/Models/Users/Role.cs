using Hexagon.Domain.Models.Common;
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
        public string RoleName { get; set; }
        #endregion

        #region Relations
        public ICollection<UserRole>? userRoles { get; set; }

        #endregion
    }
}
