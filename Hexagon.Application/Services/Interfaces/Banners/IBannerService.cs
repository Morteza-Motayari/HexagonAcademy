using Hexagon.Domain.ViewModels.Banners;

namespace Hexagon.Application.Services.Interfaces.Banners
{
    public interface IBannerService
    {
        Task<CreateBannerResult> CreateBannerAsync(CreateBannerViewModel model);
        Task<DeleteBannerResult> DeleteBannerAsync(int BannerId);
        Task<List<BannerViewModel>?> ListBannersAsync();
        Task<List<ClientSideBannerViewModel>> ClientSideBannerViewModel();
    }
}
