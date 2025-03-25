using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Gyms.GymGalleries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces.Gyms
{
    public interface IGymGalleryRepository : IGenericRepository<GymGallery>
    {
        Task<List<GymGalleryViewModel>> GetGymGalleryAsync(int gymId);
        Task<int> GymGalleryCountImagesAsync(int gymId);
    }
}
