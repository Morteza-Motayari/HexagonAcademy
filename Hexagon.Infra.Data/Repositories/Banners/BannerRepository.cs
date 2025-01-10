using Hexagon.Domain.Interfaces.Banners;
using Hexagon.Domain.Models.Banners;
using Hexagon.Domain.ViewModels.Banners;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Infra.Data.Repositories.Banners
{
    public class BannerRepository: GenericRepository<Banner>, IBannerRepository
    {
        private readonly HexagonContext _db;

        public BannerRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> ExistBannerName(string name)
        => await _db.Banners.AnyAsync(b=>b.BannerName == name);

        public async Task<List<BannerViewModel>> GetAllBannersAsync()
        => await _db.Banners.Select(b=>new BannerViewModel
        {
            BannerName = b.BannerName,
            CreatedDate = b.CreatedDate,
            BannerUrl = b.BannerUrl,
            CreatedBy = b.CreatedBy,
            Id = b.Id,
            CreatedByName=_db.Users.Where(u=>u.Id==b.CreatedBy).Select(u=>u.FirstName+" "+u.LastName).First()            
        }).ToListAsync();

        public async Task<List<ClientSideBannerViewModel>> GetBannerForClient()
        => await _db.Banners.Select(b => new ClientSideBannerViewModel
        {
            BannerUrl = b.BannerUrl,
            BannerName=b.BannerName
        }).ToListAsync();

        public async Task<int> GetBannersCount()
        => await _db.Banners.CountAsync();
    }
}
