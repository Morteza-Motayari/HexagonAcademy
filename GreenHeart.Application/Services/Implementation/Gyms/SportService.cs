using GreenHeart.Application.Extensions;
using GreenHeart.Application.Generators;
using GreenHeart.Application.Services.Interfaces.Caching;
using GreenHeart.Application.Services.Interfaces.Gyms;
using GreenHeart.Application.Statics;
using GreenHeart.Application.Statics.Caches_Constatnt;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Interfaces.Gyms;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Records;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Gyms.Sports;
using GreenHeart.Domain.ViewModels.Records.Certificates;
using GreenHeart.Domain.ViewModels.Users.Roles;
using GreenHeart.Infra.Data.Repositories;
using GreenHeart.Infra.Data.Repositories.Gyms;
using GreenHeart.Infra.Data.Repositories.Users;
using Microsoft.Extensions.Configuration;

namespace GreenHeart.Application.Services.Implementation.Gyms
{
    public class SportService(ISportRepository sportRepository,
        IUserRepository userRepository,
        ICacheService cacheService,
        IConfiguration configuration) : ISportService
    {
        private readonly bool UsingCache = configuration.GetValue<bool>("Statics:UseCaching");
        public async Task<AdminSideDetailSportViewModel?> AdminSideDetailSportAsync(int SportId)
        {
            var sport = await sportRepository.GetSportWithCertificate(SportId);
            if (sport == null)
                return null;
            AdminSideDetailSportViewModel? Detail = new()
            {
                Id = SportId,
                CertificateId = sport.CertificateId,
                Certificate = sport.Certificate?.Name,
                Title = sport.Title,
                CreatedDate = sport.CreatedDate,
                LastModifiedDate = sport.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(sport.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(sport.LastModifiedBy),
                CreatedById = sport.CreatedBy,
                LastModifiedById = sport.LastModifiedBy,
                IsDeleted = sport.IsDeleted
            };
            return Detail;
        }

        public async Task<string> CantDeleteSportForeverNowMessage(int SportId)
        {
            DateTime lastEdit = await sportRepository.GetLastModifiedDate(SportId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این رشته ورزشی تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<CreateSportResult> CreateSportAsync(CreateSportViewModel model)
        {
            if (await sportRepository.ExistSportTitle(model.Title))
                return CreateSportResult.DuplicatedTitle;

            Sport sport = new()
            {
                Title = model.Title,
                CertificateId = model.CertificateId
            };
            string slug = model.Title.GenerateSlug();
            sport.Slug = slug;

            await sportRepository.InserAsync(sport);
            await sportRepository.SaveChangeAsync();

            #region Deleting Sports Cached
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.SportRelatedKeys);
            #endregion

            return CreateSportResult.Success;
        }

        public async Task<DeleteSportResult> DeleteSportAsync(int SportId)
        {
            var Sport = await sportRepository.GetByIdAsync(SportId);
            if (Sport == null)
                return DeleteSportResult.SportNotFound;
            if (Sport.IsDeleted == true)
                return DeleteSportResult.SportAlreadyDeleted;

            Sport.IsDeleted = true;
            sportRepository.Update(Sport);
            await sportRepository.SaveChangeAsync();

            #region Deleting Sports Cached
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.SportRelatedKeys);
            #endregion

            return DeleteSportResult.Success;
        }

        public async Task<DeleteForeverSportResult> DeleteSportForever(int SportId)
        {
            var sport = await sportRepository.GetByIdAsync(SportId);

            if (sport == null)
                return DeleteForeverSportResult.NotFound;

            if (sport.IsDeleted == false)
                return DeleteForeverSportResult.FirstDeleteSimple;
            DateTime lastDate = await sportRepository.GetLastModifiedDate(SportId);
            if (lastDate.SixMonthPassed())
            {
                sportRepository.Delete(sport);
                await sportRepository.SaveChangeAsync();
                return DeleteForeverSportResult.Success;
            }
            else
            {
                return DeleteForeverSportResult.CantDeletedNow;
            }
        }

        public async Task<FilterSportViewModel> FilterSportsAsync(FilterSportViewModel filter)
        => await sportRepository.FilterSportAsync(filter);

        public async Task<List<ClientSideSportNameViewModel>?> GetActiveSportNameAsync()
        {
            string cachekey = default;
            if (UsingCache)
            {
                cachekey = CacheKeys.ActiveSports;
                if (await cacheService.ExistsAsync(cachekey))
                {
                    var cachedSports = await cacheService.GetListAsync<ClientSideSportNameViewModel>(cachekey);
                    if (cachedSports.CheckNullability())
                    {
                        return cachedSports;
                    }
                }
            }
            var sports = await sportRepository.GetActiveSportName();
            if (UsingCache && sports.CheckNullability())
                await cacheService.SetListAsync(cachekey, sports, CacheDuration.NormalCahingTime);

            return sports;
        }

        public async Task<List<ClientSideSportExisted>?> GetSportExistedAsync()
        {
            string cachekey = default;
            if (UsingCache)
            {
                cachekey = CacheKeys.SportsExisted;
                if (await cacheService.ExistsAsync(cachekey))
                {
                    var cachedSportsExisted = await cacheService.GetListAsync<ClientSideSportExisted>(cachekey);
                    if (cachedSportsExisted.CheckNullability())
                    {
                        return cachedSportsExisted;
                    }
                }
            }
            var sportsExisted = await sportRepository.GetSportExistedWithRelationAsync();
            if (UsingCache && sportsExisted.CheckNullability())
                await cacheService.SetListAsync(cachekey, sportsExisted, CacheDuration.NormalCahingTime);

            return sportsExisted;
        }


        public async Task<UpdateSportViewModel> GetSportForEdit(int SportId)
        {
            var Sport = await sportRepository.GetByIdAsync(SportId);
            if (Sport == null)
                return null;
            return new UpdateSportViewModel()
            {
                Id = Sport.Id,
                Title = Sport.Title,
                CertificateId = Sport.CertificateId,
                IsDeleted = Sport.IsDeleted
            };
        }

        public async Task<List<SportViewModel>?> ListSportsForOptionsAsync()
        => await sportRepository.GetAllSportsItemsAsync();

        public async Task<UpdateSportResult> UpdateSportAsync(UpdateSportViewModel model)
        {
            var Sport = await sportRepository.GetByIdAsync(model.Id);
            if (Sport == null)
                return UpdateSportResult.SportNotFound;
            if (await sportRepository.ExistSportTitle(model.Title, model.Id))
                return UpdateSportResult.DuplicatedTitle;

            #region Update Sport
            Sport.CertificateId = model.CertificateId;
            if (Sport.Title != model.Title)
            {
                Sport.Title = model.Title;
                string slug = model.Title.GenerateSlug();
                Sport.Slug = slug;
            }
            sportRepository.Update(Sport);
            await sportRepository.SaveChangeAsync();
            #endregion

            #region Deleting Sports Cached
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.SportRelatedKeys);
            #endregion

            return UpdateSportResult.Success;
        }

    }
}
