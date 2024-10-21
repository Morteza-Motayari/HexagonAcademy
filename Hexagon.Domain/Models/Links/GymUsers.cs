using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexagon.Domain.Models.Links
{
    public class GymUsers
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
