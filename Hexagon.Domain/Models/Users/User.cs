using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Models.Common;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Records;

namespace Hexagon.Domain.Models.Users
{
    public class User:BaseEntity<int>
    {
        #region Properties
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Slug { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? VerificationCode { get; set; }
        public string? NationalCode { get; set; }
        public string? city { get; set; }
        public string? email { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? Avatar { get; set; }
        public UserGender? Gender { get; set; }
        public UserStatus Status { get; set; }
        public UserSituation? Situation { get; set; }
        #endregion

        #region Relations
        public ICollection<UserCertificates>? UserCertificates { get; set; }
        public ICollection<Experience>? Experiences { get; set; }
        public ICollection<Staff>? staffes { get; set; }
        public ICollection<GymUser>? UserGyms { get; set; }
        public ICollection<ClassUser>? UserClasses { get; set; }
        public List<UserRole>? userRoles { get; set; }
        public ICollection<ClassComment>? ClassComments { get; set; }
        public ICollection<ClassCommentReaction>? ClassCommentReactions { get; set; }
        #endregion

    }
}
