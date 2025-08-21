using GreenHeart.Application.Convertors;
using GreenHeart.Application.Extensions;
using GreenHeart.Application.Generators;
using GreenHeart.Application.Services.Interfaces.Caching;
using GreenHeart.Application.Services.Interfaces.Gyms;
using GreenHeart.Application.Statics;
using GreenHeart.Application.Statics.Caches_Constatnt;
using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Enums.SportClasses;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Interfaces.Gyms;
using GreenHeart.Domain.Interfaces.KeyWords;
using GreenHeart.Domain.Interfaces.Links;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.ViewModels.Gyms.SportClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace GreenHeart.Application.Services.Implementation.Gyms
{
    public class SportClassService(ISportClassRepository SportClassRepository
        , IUserRepository userRepository,
        IGymRepository gymRepository,
        ISportRepository sportRepository,
        IStaffRepository staffRepository,
        IKeyWordRepository keyWordRepository,
        IClassCommentRepository classCommentRepository,
        IHttpContextAccessor httpContextAccessor,
        IClassUserRepository classUserRepository,
        ICacheService cacheService,
        IConfiguration configuration) : ISportClassService
    {
        private readonly bool UsingCache = configuration.GetValue<bool>("Statics:UseCaching");
        public async Task<AdminSideDetailSportClassViewModel?> AdminSideDetailSportClassAsync(int SportClassId)
        {
            var sportClass = await SportClassRepository.GetSportClassWithDetails(SportClassId);
            if (sportClass == null)
                return null;
            AdminSideDetailSportClassViewModel? Detail = new()
            {
                Id = SportClassId,
                Title = sportClass.Title,
                GymId = sportClass.GymId,
                gym = sportClass.gym?.Name,
                TrainerId = sportClass.TrainerId,
                trainer = await staffRepository.GetStaffNameAsync(sportClass.TrainerId),
                SportId = sportClass.SportId,
                sport = sportClass.sport?.Title,
                StartDate = sportClass.StartDate,
                RegisteredAthletes = sportClass.ClassUsers.Count(),
                StartTime = sportClass.StartTime,
                EndTime = sportClass.EndTime,
                SubscriptionFee = sportClass.SubscriptionFee,
                MaxSubscription = sportClass.MaxSubscription,
                ImageUrl = sportClass.ImageUrl,
                Gender = sportClass.Gender,
                ClassStatus = sportClass.ClassStatus,
                CreatedDate = sportClass.CreatedDate,
                LastModifiedDate = sportClass.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(sportClass.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(sportClass.LastModifiedBy),
                CreatedById = sportClass.CreatedBy,
                LastModifiedById = sportClass.LastModifiedBy,
                IsDeleted = sportClass.IsDeleted
            };
            return Detail;
        }

        public async Task<string> CantDeleteSportClassForeverNowMessage(int SportClassId)
        {
            DateTime lastEdit = await SportClassRepository.GetLastModifiedDate(SportClassId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این کلاس تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<ClientSideFilterSportClassViewModel> ClientSideFilterClasses(ClientSideFilterSportClassViewModel filter)
        {
            string cacheKey = default;
            if (UsingCache)
            {
                if (filter.Title == null && filter.Gender == null && filter.GymId == null && filter.KeyWord == null && filter.SportId == null && filter.SportSlug == null)
                {
                    cacheKey = CacheKeys.SportClassPage + filter.Page.ToString();
                    if (await cacheService.ExistsAsync(cacheKey))
                    {
                        var cachedClasses = await cacheService.GetAsync<ClientSideFilterSportClassViewModel>(cacheKey);
                        if (cachedClasses.Entities.CheckNullability())
                        {
                            return cachedClasses;
                        }
                    }
                }
            }
            var sportClasses = await SportClassRepository.ClientSideFilterClasses(filter);
            if (UsingCache)
            {
                if (cacheKey != default)
                    await cacheService.SetAsync(cacheKey, sportClasses, CacheDuration.NormalCahingTime);
            }
            return sportClasses;
        }

        public async Task<ClientSideSportClassDeatilViewModel> ClientSideSportClassViewModel(string slug)
        {
            string cacheKey = default;
            if (UsingCache)
            {
                cacheKey = CacheKeys.SportClass + slug;
                if (await cacheService.ExistsAsync(cacheKey))
                {
                    var cachedClass = await cacheService.GetAsync<ClientSideSportClassDeatilViewModel>(cacheKey);
                    if (cachedClass != null)
                    {
                        return cachedClass;
                    }
                }
            }

            var sportClass = await SportClassRepository.GetClassBySlugAsync(slug);
            if (sportClass == null)
                return null;
            if (sportClass.IsDeleted == true || sportClass.ClassStatus == SportClassStatus.NotActive)
                return null;
            var trainer = await staffRepository.GetTrainerNameAndImageAsync(sportClass.TrainerId);
            ClientSideSportClassDeatilViewModel detail = new()
            {
                Id = sportClass.Id,
                slug = sportClass.Slug,
                StartDate = sportClass.StartDate,
                StartTime = sportClass.StartTime,
                EndTime = sportClass.EndTime,
                Gender = sportClass.Gender,
                SubscriptionFee = sportClass.SubscriptionFee,
                MaxSubscription = sportClass.MaxSubscription,
                sport = sportRepository.GetSportTitle(sportClass.SportId),
                SportId = sportClass.SportId,
                Trainer = trainer.FullName,
                TrainerImage = trainer.ImageUrl,
                gym = gymRepository.GetGymName(sportClass.GymId),
                GymId = sportClass.GymId,
                ImageUrl = sportClass.ImageUrl,
                Title = sportClass.Title,
                TrainerSlug = trainer.TrainerSlug,
                KeyWords = await keyWordRepository.GetClassKeyWordsAsync(sportClass.Id),
                CommentsAmount = await classCommentRepository.ClassCommentAmountAsync(sportClass.Id),
                SportSlug = sportClass.sport.Slug
            };
            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                detail.IsRegisterted = await classUserRepository.IsUserRegisteredInClass(httpContextAccessor.HttpContext.User.GetUserId(), sportClass.Id);
                if (detail.IsRegisterted)
                {
                    detail.RegistraionDate = await classUserRepository.LastUserRegistrationDateInClass(httpContextAccessor.HttpContext.User.GetUserId(), sportClass.Id);
                }
            }
            if (UsingCache)
                await cacheService.SetAsync(cacheKey, detail, CacheDuration.NormalCahingTime);

            return detail;
        }

        public async Task<CreateSportClassResult> CreateSportClassAsync(CreateSportClassViewModel model)
        {
            if (model.StartTime >= model.EndTime)
                return CreateSportClassResult.InvalidEndTime;

            SportClass sportClass = new()
            {
                Title = model.Title,
                EndTime = model.EndTime,
                StartTime = model.StartTime,
                GymId = model.GymId,
                TrainerId = model.TrainerId,
                SportId = model.SportId,
                MaxSubscription = model.MaxSubscription,
                Gender = model.Gender,
                ClassStatus = model.ClassStatus,
                SubscriptionFee = model.SubscriptionFee
            };
            if (model.StartDate != null)
            {
                if (!model.StartDate.CheckPersianDate())
                {
                    return CreateSportClassResult.InvalidDateTime;
                }
                sportClass.StartDate = model.StartDate.ToMiladi();
            }
            string slug = model.Title.GenerateSlug();
            if (await SportClassRepository.ExistSpecificSlug(slug))
            {
                slug = await SportClassRepository.PutSpecificSlug(slug);
            }
            sportClass.Slug = slug;
            #region Image
            if (model.Image != null)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(imageName, SavingPath.SportClassPath);
                sportClass.ImageUrl = imageName;
            }
            #endregion

            await SportClassRepository.InserAsync(sportClass);
            await SportClassRepository.SaveChangeAsync();
            #region Deleting Cached List Classes
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.SportClassRelatedKeys);
            #endregion
            return CreateSportClassResult.Success;
        }

        public async Task<DeleteSportClassResult> DeleteSportClassAsync(int SportClassId)
        {
            var SportClass = await SportClassRepository.GetByIdAsync(SportClassId);
            if (SportClass == null)
                return DeleteSportClassResult.SportClassNotFound;
            if (SportClass.IsDeleted == true)
                return DeleteSportClassResult.SportClassAlreadyDeleted;

            #region Deleting SportClass Image
            if (SportClass.ImageUrl != null)
            {
                SportClass.ImageUrl.DeleteImage(SavingPath.SportClassPath);
                SportClass.ImageUrl = null;
            }
            #endregion

            SportClass.IsDeleted = true;
            SportClass.ClassStatus = SportClassStatus.Closed;
            SportClassRepository.Update(SportClass);
            await SportClassRepository.SaveChangeAsync();

            #region Deleting Cached Class
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.SportClass + SportClass.Slug);
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.SportClassRelatedKeys);
            #endregion
            return DeleteSportClassResult.Success;
        }

        public async Task<DeleteForeverSportClassResult> DeleteSportClassForever(int SportClassId)
        {
            var sportclass = await SportClassRepository.GetByIdAsync(SportClassId);

            if (sportclass == null)
                return DeleteForeverSportClassResult.NotFound;

            if (sportclass.IsDeleted == false)
                return DeleteForeverSportClassResult.FirstDeleteSimple;
            DateTime lastDate = await SportClassRepository.GetLastModifiedDate(SportClassId);
            if (lastDate.SixMonthPassed())
            {
                SportClassRepository.Delete(sportclass);
                await SportClassRepository.SaveChangeAsync();
                return DeleteForeverSportClassResult.Success;
            }
            else
            {
                return DeleteForeverSportClassResult.CantDeletedNow;
            }
        }

        public async Task<FilterSportClassAthleteViewModel> FilterSportClassAthleteAsync(FilterSportClassAthleteViewModel filter)
        => await classUserRepository.FilterSportClassAthlete(filter);

        public async Task<FilterSportClassViewModel> FilterSportClassesAsync(FilterSportClassViewModel filter)
        => await SportClassRepository.FilterSportClassAsync(filter);

        public async Task<List<ClientSideSportClassViewModel>?> GetClassesForIndexPage(FilterUserGender gender)
        {
            string cachekey = default;
            if (UsingCache)
            {
                cachekey = CacheKeys.SportClassesHomePage;
                if (await cacheService.ExistsAsync(cachekey))
                {
                    var cachedClass = await cacheService.GetListAsync<ClientSideSportClassViewModel>(cachekey);
                    if (cachedClass != null)
                        return cachedClass;
                }
            }            
            var sportClasses = await SportClassRepository.GetClassesForIndexPage(gender);
            if (UsingCache)
                await cacheService.SetListAsync(cachekey, sportClasses, CacheDuration.ClassHomeCahingTime);

            return sportClasses;
        }


        public async Task<UpdateSportClassViewModel> GetSportClassForEdit(int SportClassId)
        {
            var SportClass = await SportClassRepository.GetByIdAsync(SportClassId);
            if (SportClass == null)
                return null;
            return new UpdateSportClassViewModel()
            {
                Id = SportClass.Id,
                StartDate = SportClass.StartDate.ToShamsi(),
                StartTime = SportClass.StartTime,
                EndTime = SportClass.EndTime,
                Title = SportClass.Title,
                GymId = SportClass.GymId,
                TrainerId = SportClass.TrainerId,
                SportId = SportClass.SportId,
                SubscriptionFee = SportClass.SubscriptionFee,
                ImageUrl = SportClass.ImageUrl,
                IsDeleted = SportClass.IsDeleted,
                MaxSubscription = SportClass.MaxSubscription,
                Gender = SportClass.Gender,
                ClassStatus = SportClass.ClassStatus
            };
        }

        public async Task<UserSideFilterSportClassViewModel> GetUserSportClassesAsync(int userId, UserSideFilterSportClassViewModel filter)
        => await SportClassRepository.GetUserClassesAsync(userId, filter);

        public async Task<List<SportClassViewModel>?> ListSportClassesAsync()
        => await SportClassRepository.GetAllSportClasssAsync();

        public async Task<UpdateSportClassResult> UpdateSportClassAsync(UpdateSportClassViewModel model)
        {
            if (model.StartTime >= model.EndTime)
                return UpdateSportClassResult.InvalidEndTime;

            var SportClass = await SportClassRepository.GetByIdAsync(model.Id);
            if (SportClass == null)
                return UpdateSportClassResult.SportClassNotFound;

            #region Update SportClass
            if (model.StartDate != null)
            {
                if (!model.StartDate.CheckPersianDate())
                {
                    return UpdateSportClassResult.InvalidDateTime;
                }
                SportClass.StartDate = model.StartDate.ToMiladi();
            }
            SportClass.StartTime = model.StartTime;
            SportClass.EndTime = model.EndTime;
            SportClass.GymId = model.GymId;
            SportClass.TrainerId = model.TrainerId;
            SportClass.SportId = model.SportId;
            SportClass.ClassStatus = model.ClassStatus;
            SportClass.SubscriptionFee = model.SubscriptionFee;
            if (SportClass.Title != model.Title)
            {
                SportClass.Title = model.Title;
                string slug = model.Title.GenerateSlug();
                if (await SportClassRepository.ExistSpecificSlug(slug))
                {
                    slug = await SportClassRepository.PutSpecificSlug(slug);
                }
                SportClass.Slug = slug;
            }
            #region Update Image
            if (model.NewImage != null)
            {
                if (SportClass.ImageUrl != null)
                {
                    SportClass.ImageUrl.DeleteImage(SavingPath.SportClassPath);
                    SportClass.ImageUrl = null;
                }
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewImage.FileName);
                model.NewImage.AddImageToServer(imageName, SavingPath.SportClassPath);
                SportClass.ImageUrl = imageName;
            }
            #endregion
            SportClassRepository.Update(SportClass);
            await SportClassRepository.SaveChangeAsync();
            #endregion

            #region Deleting Cached Class
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.SportClass + SportClass.Slug);
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.SportClassRelatedKeys);
            #endregion

            return UpdateSportClassResult.Success;
        }

    }
}
