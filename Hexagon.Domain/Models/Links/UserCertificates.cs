using Hexagon.Domain.Models.Records;
using Hexagon.Domain.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexagon.Domain.Models.Links
{
    public class UserCertificates
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CertificateId { get; set; }
        public string? PlaceOftake { get; set; }
        public DateTime? DateOfTake { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User? user { get; set; }
        [ForeignKey(nameof(CertificateId))]
        public Certificate? certificate { get; set; }
        #endregion

    }
}
