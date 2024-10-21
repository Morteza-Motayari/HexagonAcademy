using Hexagon.Domain.Models.Common;
using Hexagon.Domain.Models.Records;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexagon.Domain.Models.Gyms
{
    public class Sport:BaseEntity<int>
    {
        #region Properties
        public string Title { get; set; }
        public string Slug { get; set; }
        public int CertificateId { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(CertificateId))]
        public Certificate? Certificate { get; set; }
        public ICollection<SportClass>? Classes { get; set; }
        #endregion
    }
}
