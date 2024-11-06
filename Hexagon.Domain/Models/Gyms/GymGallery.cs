using System.ComponentModel.DataAnnotations.Schema;

namespace Hexagon.Domain.Models.Gyms
{
    public class GymGallery
    {
        #region Properties
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public int GymId { get; set; }
        public string ImageUrl { get; set; }
        public string ImageTitle { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(GymId))]
        public Gym gym { get; set; }
        #endregion
    }
}
