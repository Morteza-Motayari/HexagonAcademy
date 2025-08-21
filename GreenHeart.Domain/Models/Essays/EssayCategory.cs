using GreenHeart.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Models.Essays
{
    public class EssayCategory : BaseEntity<int>
    {
        #region Properties
        public string Title { get; set; }
        public string  Slug { get; set; }
        public int? EssayCategoryParentId { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(EssayCategoryParentId))]
        public EssayCategory? CategoryParent { get; set; }
        public ICollection<EssayCategory>? CategoryChilds { get; set; }
        public ICollection<Essay>? Essays { get; set; }
        #endregion
    }
}
