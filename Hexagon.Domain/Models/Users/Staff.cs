using Hexagon.Domain.Models.Common;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.Links;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexagon.Domain.Models.Users
{
    public class Staff:BaseEntity<int>
    {
        #region Properties
        public int Salary { get; set; }
        public string Position { get; set; }
        public int UserId { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User user { get; set; }
        public ICollection<GymStaff>? StaffGyms { get; set; }
        public ICollection<SportClass>? SportClasses { get; set; }
        public ICollection<UserCertificates>? UserCertificates { get; set; }
        public List<UserRole>? userRoles { get; set; }

        #endregion
    }
}
