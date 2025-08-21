using GreenHeart.Domain.Models.Common;
using GreenHeart.Domain.Models.Essays;
using GreenHeart.Domain.Models.Gyms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Models.KeyWords
{
    public class KeyWord:BaseEntity<int>
    {
        #region Properties
        public string Key { get; set; }
        public int? ClassId { get; set; }
        public int? EssayId { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(ClassId))]
        public SportClass? sportClass { get; set; }
        [ForeignKey(nameof(EssayId))]
        public Essay? Essay { get; set; }
        #endregion
    }
}
