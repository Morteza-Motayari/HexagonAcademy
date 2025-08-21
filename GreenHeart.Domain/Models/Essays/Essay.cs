using GreenHeart.Domain.Models.Common;
using GreenHeart.Domain.Models.KeyWords;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GreenHeart.Domain.Models.Essays
{
    public class Essay:BaseEntity<int>
    {
        #region Properties
        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        [StringLength(500)]
        public string Excerpt { get; set; }
        [StringLength(100)]
        public string Slug { get; set; }
        public int  EssayCagtegoryId { get; set; }
        public string? ImageUrl { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(EssayCagtegoryId))]
        public EssayCategory? essayCategory { get; set; }
        public ICollection<KeyWord>? keyWords { get; set; }
        #endregion
    }
}
