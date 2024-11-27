using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Application.Statics;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.GymGalleries;

namespace Hexagon.Application.Services.Implementation.Gyms
{
    public class GymGalleryService(IGymGalleryRepository gymGalleryRepository,IGymRepository gymRepository) : IGymGalleryService
    {
        public async Task<CreateGymGalleryResult> CreateGymGalleryAsync(CreateGymGalleryViewModel model)
        {
            if(model.Image==null)
                return CreateGymGalleryResult.ImageNull;
            if (await gymGalleryRepository.GymGalleryCountImagesAsync(model.GymId) >= 8)
                return CreateGymGalleryResult.MaxImagesForGym;

            GymGallery gymGallery = new()
            {
                CreatedDate = DateTime.Now,
                ImageTitle = model.ImageTitle,
                GymId = model.GymId
            };
            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
            model.Image.AddImageToServer(imageName, SavingPath.GymGalleryPath);
            gymGallery.ImageUrl = imageName;
            await gymGalleryRepository.InserAsync(gymGallery);
            await gymGalleryRepository.SaveChangeAsync();
            return CreateGymGalleryResult.Success;
        }

        public async Task<DeleteGymGalleryResult> DeleteGymGalleryAsync(int GymGalleryId)
        {
            GymGallery? gymGallery=await gymGalleryRepository.GetByIdAsync(GymGalleryId);
            if (gymGallery == null)
                return DeleteGymGalleryResult.GymImageNotFound;
            #region Deleting GymGallery Image
            if (gymGallery.ImageUrl != null)
                gymGallery.ImageUrl.DeleteImage(SavingPath.GymGalleryPath);
            #endregion
            gymGalleryRepository.Delete(gymGallery);
            await gymGalleryRepository.SaveChangeAsync();
            return DeleteGymGalleryResult.Success;
        }

        public async Task<bool> GymExistForGalleyAsync(int gymId)
        => await gymRepository.ExistGymAsync(gymId);

        public async Task<List<GymGalleryViewModel>?> ListGymGallerysAsync(int gymId)
        => await gymGalleryRepository.GetGymGalleryAsync(gymId);
    }
}
