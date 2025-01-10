using Hexagon.Domain.Models.Banners;
using Hexagon.Domain.Models.Contact_Us;
using Hexagon.Domain.ViewModels.Banners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Banners
{
    public interface IBannerRepository: IGenericRepository<Banner>
    {
        Task<int> GetBannersCount();
        Task<bool> ExistBannerName(string name);
        Task<List<BannerViewModel>> GetAllBannersAsync();
        Task<List<ClientSideBannerViewModel>> GetBannerForClient();
    }
}
