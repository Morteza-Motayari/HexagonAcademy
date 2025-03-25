using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenHeart.Domain.Models.Links
{
    public class ClassUser
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SportClassId { get; set; }
        public DateTime SubscriptionDate { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User user { get; set; }
        [ForeignKey(nameof(SportClassId))]
        public SportClass SportClass { get; set; }
        #endregion
    }
}
