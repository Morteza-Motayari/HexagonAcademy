using GreenHeart.Domain.Models.Common;
using GreenHeart.Domain.Models.Links;
using GreenHeart.Domain.Models.Users;

namespace GreenHeart.Domain.Models.Gyms
{
    public class Gym:BaseEntity<int>
    {
        #region Properties
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Address { get; set; }
        public int? Area { get; set; }
        public string? ConstantPhone { get; set; }
        public string? ImageUrl{ get; set; }

        #endregion

        #region Relations
        public ICollection<GymUser>? GymUsers { get; set; }
        public ICollection<GymStaff>? GymStaffs { get; set; }
        public ICollection<GymGallery>? Gallery { get; set; }
        public ICollection<SportClass>? SportClasses { get; set; }
        #endregion
    }
}
