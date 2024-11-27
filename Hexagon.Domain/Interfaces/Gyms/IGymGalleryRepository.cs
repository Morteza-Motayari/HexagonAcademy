using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Gyms.GymGalleries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Gyms
{
    public interface IGymGalleryRepository : IGenericRepository<GymGallery>
    {
        Task<List<GymGalleryViewModel>> GetGymGalleryAsync(int gymId);
        Task<int> GymGalleryCountImagesAsync(int gymId);
    }
}
