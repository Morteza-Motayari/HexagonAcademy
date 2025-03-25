using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Models.Links
{
    public class GymStaff
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        public int StaffId { get; set; }
        public int GymId { get; set; }
        public DateTime RegisteredDate { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(StaffId))]
        public Staff staff { get; set; }
        [ForeignKey(nameof(GymId))]
        public Gym gym { get; set; }
        #endregion
    }
}
