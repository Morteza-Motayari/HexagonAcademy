using GreenHeart.Domain.Models.Common;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Links;
using GreenHeart.Domain.Models.Records;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenHeart.Domain.Models.Users
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
        public ICollection<RecordCategory> RecordCategories { get; set; }

        #endregion
    }
}
