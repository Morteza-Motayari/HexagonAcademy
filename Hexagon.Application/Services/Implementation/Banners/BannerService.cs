using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Banners;
using Hexagon.Application.Statics;
using Hexagon.Domain.Interfaces.Banners;
using Hexagon.Domain.Models.Banners;
using Hexagon.Domain.ViewModels.Banners;

namespace Hexagon.Application.Services.Implementation.Banners
{
    public class BannerService(IBannerRepository bannerRepository) : IBannerService
    {
        public async Task<List<ClientSideBannerViewModel>> ClientSideBannerViewModel()
        => await bannerRepository.GetBannerForClient();

        public async Task<CreateBannerResult> CreateBannerAsync(CreateBannerViewModel model)
        {
            if (await bannerRepository.GetBannersCount() >= 10)
            {
                return CreateBannerResult.MaximumBannerReached;
            }

            if (await bannerRepository.ExistBannerName(model.BannerName))
            {
                return CreateBannerResult.DuplicatedBannerName;
            }
            Banner banner = new()
            {
                BannerName = model.BannerName
            };
            string bannerUrl = Guid.NewGuid().ToString() + Path.GetExtension(model.BannerImage.FileName);
            model.BannerImage.AddImageToServer(bannerUrl, SavingPath.BannerPath);
            banner.BannerUrl = bannerUrl;
            await bannerRepository.InserAsync(banner);
            await bannerRepository.SaveChangeAsync();
            return CreateBannerResult.Success;
        }

        public async Task<DeleteBannerResult> DeleteBannerAsync(int BannerId)
        {
            Banner? banner = await bannerRepository.GetByIdAsync(BannerId);
            if (banner == null)
                return DeleteBannerResult.BannerNotFound;
            #region Deleting Banner Image
            if (banner.BannerUrl != null)
                banner.BannerUrl.DeleteImage(SavingPath.BannerPath);
            #endregion
            bannerRepository.Delete(banner);
            await bannerRepository.SaveChangeAsync();
            return DeleteBannerResult.Success;
        }

        public async Task<List<BannerViewModel>?> ListBannersAsync()
        => await bannerRepository.GetAllBannersAsync();
    }
}
