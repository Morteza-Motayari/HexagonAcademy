using Hexagon.Domain.Models.Common;
using Hexagon.Domain.Models.Gyms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Models.KeyWords
{
    public class KeyWord:BaseEntity<int>
    {
        #region Properties
        public string Key { get; set; }
        public int? ClassId { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(ClassId))]
        public SportClass? sportClass { get; set; }
        #endregion
    }
}
