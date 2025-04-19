using GreenHeart.Domain.Models.Common;
using GreenHeart.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Models.Records
{
    public class RecordCategory:BaseEntity<int>
    {
        #region Properties
        public string Title { get; set; }
        public int StaffId { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(StaffId))]
        public Staff Trainer { get; set; }
        public ICollection<Experience>? Experiences { get; set; }
        #endregion
    }
}
