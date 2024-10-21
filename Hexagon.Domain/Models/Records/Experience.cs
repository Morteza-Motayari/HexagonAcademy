using Hexagon.Domain.Models.Common;
using Hexagon.Domain.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexagon.Domain.Models.Records
{
    public class Experience:BaseEntity<int>
    {
        #region Properties
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string? Company { get; set; }
        public string? Detail { get; set; }
        public TimeSpan HowLong { get; set; }
        public int? CertificateId { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User? user { get; set; }
        [ForeignKey(nameof(CertificateId))]
        public Certificate? certificate { get; set; }
        #endregion
    }
}
