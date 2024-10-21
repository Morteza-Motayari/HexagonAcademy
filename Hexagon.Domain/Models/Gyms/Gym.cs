using Hexagon.Domain.Models.Common;
using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Users;

namespace Hexagon.Domain.Models.Gyms
{
    public class Gym:BaseEntity<int>
    {
        #region Properties
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Address { get; set; }
        public int? Area { get; set; }
        public string? ConstantPhone { get; set; }

        #endregion

        #region Relations
        public ICollection<GymUsers>? GymUsers { get; set; }
        public ICollection<Staff>? Staffs { get; set; }
        public ICollection<GymGallery>? Gallery { get; set; }
        public ICollection<SportClass>? SportClasses { get; set; }
        #endregion
    }
}
