using GreenHeart.Domain.Enums.SportClasses;
using GreenHeart.Domain.Enums.Users;
using GreenHeart.Domain.Models.Common;
using GreenHeart.Domain.Models.KeyWords;
using GreenHeart.Domain.Models.Links;
using GreenHeart.Domain.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenHeart.Domain.Models.Gyms
{
    public class SportClass:BaseEntity<int>
    {
        #region Properties
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public double SubscriptionFee { get; set; }
        public int SportId { get; set; }
        public int GymId {  get; set; }
        public int TrainerId {  get; set; }
        public string? ImageUrl { get; set; }
        public int MaxSubscription {  get; set; }
        public UserGender Gender { get; set; }
        public SportClassStatus ClassStatus { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(SportId))]
        public Sport? sport { get; set; }
        [ForeignKey(nameof(GymId))]
        public Gym? gym { get; set; }
        [ForeignKey(nameof(TrainerId))]
        public Staff? Trainer { get; set; }
        public ICollection<ClassUser>? ClassUsers { get; set; }
        public ICollection<KeyWord>? KeyWords { get; set; }
        public ICollection<ClassComment>? ClassComments { get; set; }
        public ICollection<ClassCommentReaction>? ClassCommentReactions { get; set; }
        #endregion
    }
}
