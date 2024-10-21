using Hexagon.Domain.Models.Common;
using Hexagon.Domain.Models.Gyms;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexagon.Domain.Models.Users
{
    public class Staff:BaseEntity<int>
    {
        #region Properties
        public int Salary { get; set; }
        public string Position { get; set; }
        public int UserId { get; set; }
        public int GymId {  get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User user { get; set; }
        [ForeignKey(nameof(GymId))]
        public Gym gym { get; set; }
        public ICollection<SportClass>? SportClasses { get; set; }
        #endregion
    }
}
