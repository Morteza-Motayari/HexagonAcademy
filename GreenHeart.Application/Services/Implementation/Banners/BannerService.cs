using GreenHeart.Application.Extensions;
using GreenHeart.Application.Services.Interfaces.Banners;
using GreenHeart.Application.Services.Interfaces.Caching;
using GreenHeart.Application.Statics;
using GreenHeart.Application.Statics.Caches_Constatnt;
using GreenHeart.Domain.Interfaces.Banners;
using GreenHeart.Domain.Models.Banners;
using GreenHeart.Domain.ViewModels.Banners;

namespace GreenHeart.Application.Services.Implementation.Banners
{
    public class BannerService(IBannerRepository bannerRepository,ICacheService cacheService) : IBannerService
    {
        public async Task<List<ClientSideBannerViewModel>> ClientSideBannerViewModel()
        {
            string cacheKey = CacheKeys.AllBanners;
            if(await cacheService.ExistsAsync(cacheKey))
            {
                var cachedBanners = await cacheService.GetListAsync<ClientSideBannerViewModel>(cacheKey);
                if (cachedBanners.CheckNullability())
                {
                    return cachedBanners;
                }
            }            

            var banners = await bannerRepository.GetBannerForClient();
            if (banners.CheckNullability())
            {
                await cacheService.SetListAsync(cacheKey, banners, CacheDuration.NormalCahingTime);
            }

            return banners;
        }

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

            #region Deleting Cached Banners
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.AllBanners);
            #endregion


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

            #region Deleting Cached Banners
            await CacheExtensions.InvalidateCacheKey(cacheService,CacheKeys.AllBanners);
            #endregion

            return DeleteBannerResult.Success;
        }

        public async Task<List<BannerViewModel>?> ListBannersAsync()
        => await bannerRepository.GetAllBannersAsync();
    }
}
