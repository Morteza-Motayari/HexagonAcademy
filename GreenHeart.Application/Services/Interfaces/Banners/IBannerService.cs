using GreenHeart.Domain.ViewModels.Banners;

namespace GreenHeart.Application.Services.Interfaces.Banners
{
    public interface IBannerService
    {
        Task<CreateBannerResult> CreateBannerAsync(CreateBannerViewModel model);
        Task<DeleteBannerResult> DeleteBannerAsync(int BannerId);
        Task<List<BannerViewModel>?> ListBannersAsync();
        Task<List<ClientSideBannerViewModel>> ClientSideBannerViewModel();
    }
}
