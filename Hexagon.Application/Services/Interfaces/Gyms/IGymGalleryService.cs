using Hexagon.Domain.ViewModels.Gyms.GymGalleries;

namespace Hexagon.Application.Services.Interfaces.Gyms
{
    public interface IGymGalleryService
    {
        Task<CreateGymGalleryResult> CreateGymGalleryAsync(CreateGymGalleryViewModel model);
        Task<DeleteGymGalleryResult> DeleteGymGalleryAsync(int GymGalleryId);
        Task<List<GymGalleryViewModel>?> ListGymGallerysAsync(int gymId);
    }
}
