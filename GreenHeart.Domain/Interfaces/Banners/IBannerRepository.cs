using GreenHeart.Domain.Models.Banners;
using GreenHeart.Domain.Models.Contact_Us;
using GreenHeart.Domain.ViewModels.Banners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces.Banners
{
    public interface IBannerRepository: IGenericRepository<Banner>
    {
        Task<int> GetBannersCount();
        Task<bool> ExistBannerName(string name);
        Task<List<BannerViewModel>> GetAllBannersAsync();
        Task<List<ClientSideBannerViewModel>> GetBannerForClient();
    }
}
