using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenHeart.Domain.Models.Links
{
    public class GymUser
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GymId { get; set; }
        public DateTime RegisteredDate { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User user { get; set; }
        [ForeignKey(nameof(GymId))]
        public Gym gym { get; set; }
        #endregion
    }
}
