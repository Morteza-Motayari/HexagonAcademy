using GreenHeart.Domain.Enums.SportClasses;
using GreenHeart.Domain.Models.Common;
using GreenHeart.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Models.Gyms
{
    public class ClassComment:BaseEntity<int>
    {
        #region Properties
        public int SportClassId { get; set; }
        public string Comment { get; set; }
        public ClassCommentPending CommentStatus { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(CreatedBy))]
        public User? User { get; set; }
        [ForeignKey(nameof(SportClassId))]
        public SportClass sportClass { get; set; }
        public ICollection<ClassCommentReaction> CommentReactions { get; set; }
        #endregion
    }
}
