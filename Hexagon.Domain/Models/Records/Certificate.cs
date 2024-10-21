using Hexagon.Domain.Models.Common;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.Links;

namespace Hexagon.Domain.Models.Records
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
