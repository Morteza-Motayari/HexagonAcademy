using GreenHeart.Domain.ViewModels.Gyms.GymGalleries;

namespace GreenHeart.Application.Services.Interfaces.Gyms
{
    public interface IGymGalleryService
    {
        Task<CreateGymGalleryResult> CreateGymGalleryAsync(CreateGymGalleryViewModel model);
        Task<DeleteGymGalleryResult> DeleteGymGalleryAsync(int GymGalleryId);
        Task<List<GymGalleryViewModel>?> ListGymGallerysAsync(int gymId);
        Task<bool> GymExistForGalleyAsync(int gymId);
    }
}
