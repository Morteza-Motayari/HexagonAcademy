using GreenHeart.Domain.Models.Common;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Links;

namespace GreenHeart.Domain.Models.Records
{
    public class Certificate:BaseEntity<int>
    {
        #region Properties
        public string Name { get; set; }
        public string? Details { get; set; }
        #endregion

        #region Relations
        public ICollection<UserCertificates>? UserCertificates { get; set; }
        public ICollection<Sport>? Sports { get; set; }
        #endregion

    }
}
