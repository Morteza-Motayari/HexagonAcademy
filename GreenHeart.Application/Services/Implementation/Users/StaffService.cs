using GreenHeart.Application.Convertors;
using GreenHeart.Application.Extensions;
using GreenHeart.Application.Generators;
using GreenHeart.Application.Services.Interfaces.Caching;
using GreenHeart.Application.Services.Interfaces.Users;
using GreenHeart.Application.Statics.Caches_Constatnt;
using GreenHeart.Domain.Enums.Users;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Interfaces.Gyms;
using GreenHeart.Domain.Interfaces.Links;
using GreenHeart.Domain.Interfaces.Users;
using GreenHeart.Domain.Models.Links;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Users.Staffs.Caders;
using GreenHeart.Domain.ViewModels.Users.Staffs.Trainers;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;

namespace GreenHeart.Application.Services.Implementation.Users
{
    public class StaffService(IStaffRepository staffRepository
        , IUserCertificatesRepository userCertificatesRepository
        , IUserRepository userRepository
        , ICertificateRepository certificateRepository,
        ISportRepository sportRepository,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository,
        IExperienceRepository experienceRepository,
        ICacheService cacheService,
        IConfiguration configuration) : IStaffService
    {
        private readonly bool UsingCache = configuration.GetValue<bool>("Statics:UseCaching");
        public async Task<AdminSideDetailCaderViewModel?> AdminSideDetailCaderAsync(int CaderId)
        {
            var cader = await staffRepository.GetStaffWithUser(CaderId);
            if (cader == null)
                return null;
            AdminSideDetailCaderViewModel? Detail = new()
            {
                Id = CaderId,
                FullName = cader.user.GetUserName(),
                Avatar = cader.user.Avatar,
                Gender = cader.user.Gender,
                email = cader.user.email,
                city = cader.user.city,
                NationalCode = cader.user.NationalCode,
                PhoneNumber = cader.user.PhoneNumber,
                Salary = cader.Salary,
                Position = cader.Position,
                UserId = cader.user.Id,
                CaderRoles = await roleRepository.getCaderRoles(cader.user.Id, CaderId),
                IsDeleted = cader.IsDeleted,
                CreatedById = cader.CreatedBy,
                LastModifiedById = cader.LastModifiedBy,
                BirthDay = cader.user.BirthDay?.ToShamsi(),
                CreatedDate = cader.CreatedDate,
                CreatedBy = await userRepository.GetJustUserName(cader.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(cader.LastModifiedBy),
                LastModifiedDate = cader.LastModifiedDate
            };

            return Detail;
        }
        public async Task<AdminSideDetailTrainerViewModel?> AdminSideDetailTrainerAsync(int TrainerId)
        {
            var trainer = await staffRepository.GetStaffWithUser(TrainerId);
            if (trainer == null)
                return null;
            AdminSideDetailTrainerViewModel? Detail = new()
            {
                Id = TrainerId,
                FullName = trainer.user.GetUserName(),
                Avatar = trainer.user.Avatar,
                Gender = trainer.user.Gender,
                email = trainer.user.email,
                city = trainer.user.city,
                NationalCode = trainer.user.NationalCode,
                PhoneNumber = trainer.user.PhoneNumber,
                Salary = trainer.Salary,
                Position = trainer.Position,
                UserId = trainer.user.Id,
                TrainerCertificates = await certificateRepository.GetTrainerCertificate(trainer.user.Id, TrainerId),
                IsDeleted = trainer.IsDeleted,
                CreatedById = trainer.CreatedBy,
                LastModifiedById = trainer.LastModifiedBy,
                BirthDay = trainer.user.BirthDay?.ToShamsi(),
                CreatedDate = trainer.CreatedDate,
                CreatedBy = await userRepository.GetJustUserName(trainer.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(trainer.LastModifiedBy),
                LastModifiedDate = trainer.LastModifiedDate
            };

            return Detail;
        }

        public async Task<string> CantDeleteCaderForeverNowMessage(int CaderId)
        {
            DateTime lastEdit = await staffRepository.GetLastModifiedDate(CaderId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این کادر تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<string> CantDeleteTrainerForeverNowMessage(int TrainerId)
        {
            DateTime lastEdit = await staffRepository.GetLastModifiedDate(TrainerId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این مربی تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<ClientSideFilterTrainerViewModel> ClientSideFilterTrainerAsync(ClientSideFilterTrainerViewModel filter)
        {
            string cachekey = CacheKeys.TrainerPage + filter.Page.ToString();
            if (await cacheService.ExistsAsync(cachekey))
            {
                var cachedTrainer = await cacheService.GetAsync<ClientSideFilterTrainerViewModel>(cachekey);
                if (cachedTrainer.Entities.CheckNullability())
                {
                    return cachedTrainer;
                }
            }
            var trainers = await userRepository.ClientSideFilterTrainer(filter);
            await cacheService.SetAsync(cachekey, trainers, CacheDuration.SportClassCahingTime);
            return trainers;
        }


        public async Task<CreateCaderResult> CreateCaderAsync(CreateCaderViewModel model)
        {
            var user = await userRepository.GetByIdAsync(model.UserId);
            if (user == null)
                return CreateCaderResult.UserNotFound;
            if (await staffRepository.DuplicatedStaffPositionAsync(model.Position, model.UserId))
                return CreateCaderResult.DuplicatedPosition;
            //in below code we check out if there is any staff existed for the user that have the same role chosed for the new staff
            if (await staffRepository.ExistStaffForUser(model.UserId))
            {
                List<int> staffIds = await staffRepository.GetUserStaffIds(model.UserId);
                foreach (int staffId in staffIds)
                {
                    foreach (var role in model.CaderRoleIds)
                    {
                        if (await userRoleRepository.ExistRoleForUser(model.UserId, role, staffId))
                        {
                            return CreateCaderResult.ExistRoleForUser;
                        }
                    }
                }
            }

            #region Changing User Situation
            if (user.Situation != UserSituation.Trainer)
            {
                user.Situation = UserSituation.Cader;
                userRepository.Update(user);
            }
            #endregion
            Staff cader = new()
            {
                Position = model.Position,
                UserId = model.UserId
            };
            try
            {
                cader.Salary = int.Parse(model.Salary);
            }
            catch
            {
                return CreateCaderResult.InValidSalary;
            }
            await staffRepository.InserAsync(cader);
            await staffRepository.SaveChangeAsync();
            if (model.CaderRoleIds.CheckNullability())
            {
                foreach (var role in model.CaderRoleIds)
                {
                    await userRoleRepository.InserAsync(new UserRole
                    {
                        UserId = model.UserId,
                        RoleId = role,
                        CaderId = cader.Id
                    });
                }
                await userRoleRepository.SaveChangeAsync();
            }
            #region Deleting Cached cader
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.CaderAboutUsPage);
            #endregion
            return CreateCaderResult.Success;
        }

        public async Task<CreateTrainerResult> CreateTrainerAsync(CreateTrainerViewModel model)
        {
            var user = await userRepository.GetByIdAsync(model.UserId);
            if (user == null)
                return CreateTrainerResult.UserNotFound;
            if (await staffRepository.DuplicatedStaffPositionAsync(model.Position, model.UserId))
                return CreateTrainerResult.DuplicatedPosition;
            //in below code we check out if there is any staff existed for the user that have the same certificate chosed for the new staff
            if (await staffRepository.ExistStaffForUser(model.UserId))
            {
                List<int> staffIds = await staffRepository.GetUserStaffIds(model.UserId);
                foreach (int staffId in staffIds)
                {
                    foreach (var certificate in model.TrainerCertificatesIds)
                    {
                        if (await userCertificatesRepository.ExistCertificateForUser(model.UserId, certificate, staffId))
                        {
                            return CreateTrainerResult.ExistCertificateForUser;
                        }
                    }
                }
            }

            Staff trainer = new()
            {
                Position = model.Position,
                UserId = model.UserId
            };
            try
            {
                trainer.Salary = int.Parse(model.Salary);
            }
            catch
            {
                return CreateTrainerResult.InValidSalary;
            }

            #region Adding Slug to user
            string slug = user.GetUserName().GenerateSlug();
            if (await userRepository.ExistSpecificSlug(slug))
            {
                slug = await userRepository.PutSpecificSlug(slug);
            }
            user.Slug = slug;
            #endregion

            #region Changing User Situation
            user.Situation = UserSituation.Trainer;
            userRepository.Update(user);
            await userRepository.SaveChangeAsync();
            #endregion

            await staffRepository.InserAsync(trainer);
            await staffRepository.SaveChangeAsync();
            if (model.TrainerCertificatesIds.CheckNullability())
            {
                foreach (var certificate in model.TrainerCertificatesIds)
                {
                    await userCertificatesRepository.InserAsync(new UserCertificates
                    {
                        UserId = model.UserId,
                        CertificateId = certificate,
                        StaffId = trainer.Id
                    });
                }
                await userCertificatesRepository.SaveChangeAsync();
            }
            #region Deleting Cached Trainers
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.TrainerRelatedKeys);
            #endregion
            return CreateTrainerResult.Success;
        }

        public async Task<DeleteCaderResult> DeleteCaderAsync(int CaderId)
        {
            var cader = await staffRepository.GetByIdAsync(CaderId);
            if (cader == null)
                return DeleteCaderResult.CaderNotFound;
            if (cader.IsDeleted == true)
                return DeleteCaderResult.CaderAlreadyDeleted;

            cader.IsDeleted = true;
            staffRepository.Update(cader);
            await staffRepository.SaveChangeAsync();
            #region Deleting Cached cader
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.CaderAboutUsPage);
            #endregion
            return DeleteCaderResult.Success;
        }

        public async Task<DeleteForeverCaderResult> DeleteCaderForever(int CaderId)
        {
            DateTime lastDate = await staffRepository.GetLastModifiedDate(CaderId);
            if (lastDate.SixMonthPassed())
            {
                var cader = await staffRepository.GetByIdAsync(CaderId);

                if (cader == null)
                    return DeleteForeverCaderResult.NotFound;

                if (cader.IsDeleted == false)
                    return DeleteForeverCaderResult.FirstDeleteSimple;

                staffRepository.Delete(cader);
                await staffRepository.SaveChangeAsync();
                return DeleteForeverCaderResult.Success;
            }
            else
            {
                return DeleteForeverCaderResult.CantDeletedNow;
            }
        }

        public async Task<DeleteTrainerResult> DeleteTrainerAsync(int TrainerId)
        {
            var trainer = await staffRepository.GetByIdAsync(TrainerId);
            if (trainer == null)
                return DeleteTrainerResult.TrainerNotFound;
            if (trainer.IsDeleted == true)
                return DeleteTrainerResult.TrainerAlreadyDeleted;

            trainer.IsDeleted = true;
            staffRepository.Update(trainer);
            await staffRepository.SaveChangeAsync();
            #region Deleteing Cached trainer
            string trainerSlug = await userRepository.GetStaffSlugByIdAsync(trainer.UserId);
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.Trainer + trainerSlug);
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.TrainerRelatedKeys);
            #endregion
            return DeleteTrainerResult.Success;
        }

        public async Task<DeleteForeverTrainerResult> DeleteTrainerForever(int TrainerId)
        {
            DateTime lastDate = await staffRepository.GetLastModifiedDate(TrainerId);
            if (lastDate.SixMonthPassed())
            {
                var trainer = await staffRepository.GetByIdAsync(TrainerId);

                if (trainer == null)
                    return DeleteForeverTrainerResult.NotFound;

                if (trainer.IsDeleted == false)
                    return DeleteForeverTrainerResult.FirstDeleteSimple;

                staffRepository.Delete(trainer);
                await staffRepository.SaveChangeAsync();
                return DeleteForeverTrainerResult.Success;
            }
            else
            {
                return DeleteForeverTrainerResult.CantDeletedNow;
            }
        }

        public async Task<FilterCaderViewModel> FilterCadersAsync(FilterCaderViewModel filter)
        => await staffRepository.FilterCadersAsync(filter);

        public async Task<FilterTrainerViewModel> FilterTrainersAsync(FilterTrainerViewModel filter)
        => await staffRepository.FilterTrainersAsync(filter);

        public async Task<UpdateCaderViewModel?> GetCaderForEdit(int CaderId)
        {
            var cader = await staffRepository.GetByIdAsync(CaderId);
            if (cader == null)
                return null;
            return new UpdateCaderViewModel()
            {
                Id = cader.Id,
                CaderRoleIds = await userRoleRepository.GetCaderRoleIdsAsync(CaderId),
                Position = cader.Position,
                Salary = cader.Salary.ToString(),
                UserId = cader.UserId,
                IsDeleted = cader.IsDeleted,
                CaderName = await userRepository.GetJustUserName(cader.UserId)
            };
        }

        public async Task<List<ClientSideCaderViewModel>?> GetCadersForAbouUsPageAsync()
        {
            string cachekey = default;
            if (UsingCache)
            {
                cachekey = CacheKeys.CaderAboutUsPage;
                if (await cacheService.ExistsAsync(cachekey))
                {
                    var cachedCader = await cacheService.GetListAsync<ClientSideCaderViewModel>(cachekey);
                    if (cachedCader.CheckNullability())
                        return cachedCader;
                }
            }
            var caders = await userRepository.GetCadersForAbouUsPage();
            if (UsingCache)
                await cacheService.SetListAsync(cachekey, caders, CacheDuration.ClassHomeCahingTime);
            return caders;
        }

        public async Task<string> GetStaffNameAsync(int staffId)
        => await staffRepository.GetStaffNameAsync(staffId);

        public async Task<ClientSideTrainerDetailViewModel?> GetTrainerDetailAsync(string slug)
        {
            string cachekey = default;
            if (UsingCache)
            {
                cachekey = CacheKeys.Trainer + slug;
                if (await cacheService.ExistsAsync(cachekey))
                {
                    var cachedTrainer = await cacheService.GetAsync<ClientSideTrainerDetailViewModel>(cachekey);
                    if (cachedTrainer != null)
                        return cachedTrainer;
                }
            }
            var user = await userRepository.GetUserBySlug(slug);
            if (user == null)
                return null;
            var trainer = await staffRepository.GetStaffByUserSlug(slug);
            if (trainer == null)
                return null;
            var trainerDetail = new ClientSideTrainerDetailViewModel()
            {
                Avatar = user.Avatar,
                FullName = user.GetUserName(),
                email = user.email,
                Slug = user.Slug,
                Position = trainer.Position,
                Experiences = await experienceRepository.GetTrainerExperienceForClientSide(trainer.Id)
            };
            if (UsingCache)
                await cacheService.SetAsync(cachekey, trainerDetail, CacheDuration.SportClassCahingTime);

            return trainerDetail;
        }

        public async Task<UpdateTrainerViewModel?> GetTrainerForEdit(int TrainerId)
        {
            var trainer = await staffRepository.GetByIdAsync(TrainerId);
            if (trainer == null)
                return null;
            return new UpdateTrainerViewModel()
            {
                Id = trainer.Id,
                TrainerCertificatesIds = await userCertificatesRepository.GetStaffCertificateIdsAsync(TrainerId),
                Position = trainer.Position,
                Salary = trainer.Salary.ToString(),
                UserId = trainer.UserId,
                IsDeleted = trainer.IsDeleted,
                TrainerName = await userRepository.GetJustUserName(trainer.UserId)
            };
        }

        public async Task<List<ClientSideTrainerViewModel>> GetTrainersForHomePageAsync()
        {
            string cachekey = default;
            if (UsingCache)
            {
                cachekey = CacheKeys.TrainerHomePage;
                if (await cacheService.ExistsAsync(cachekey))
                {
                    var cachedTrainer = await cacheService.GetListAsync<ClientSideTrainerViewModel>(cachekey);
                    if (cachedTrainer.CheckNullability())
                        return cachedTrainer;
                }
            }
            var trainers = await userRepository.GetTrainersForHomePage();
            if (UsingCache)
                await cacheService.SetListAsync(cachekey, trainers, CacheDuration.ClassHomeCahingTime);

            return trainers;
        }


        public async Task<TrainerViewModel> GetTrainerWithName(int trainerId)
        {
            var trainer = await staffRepository.GetByIdAsync(trainerId);
            if (trainer == null)
                return null;
            return new TrainerViewModel()
            {
                Id = trainer.Id,
                FullName = await userRepository.GetJustUserName(trainer.UserId)
            };
        }

        public async Task<int> GetUserIdByStaffIdAsync(int staffId)
        => await staffRepository.GetUserIdByStaffId(staffId);

        public async Task<List<TrainerViewModel>?> ListTrainerForEditItemsAsync(UserGender gender, int sportId)
        {
            int sportCertifiacetId = await sportRepository.GetSportCertifiacetId(sportId);
            var list = await staffRepository.ListSuitableTrainersForEditClassAsync(gender, sportCertifiacetId);
            return list;
        }

        public async Task<ReadOnlyCollection<TrainerViewModel>?> ListTrainerForItemsAsync(UserGender gender, int sportId)
        {
            int sportCertifiacetId = await sportRepository.GetSportCertifiacetId(sportId);
            var list = staffRepository.ListSuitableTrainersForClassAsync(gender, sportCertifiacetId);
            return list;
        }

        public async Task<UpdateCaderResult> UpdateCaderAsync(UpdateCaderViewModel model)
        {
            var cader = await staffRepository.GetByIdAsync(model.Id);
            if (cader == null)
                return UpdateCaderResult.CaderNotFound;

            if (await staffRepository.DuplicatedStaffPositionAsync(model.Position, model.UserId, model.Id))
                return UpdateCaderResult.DuplicatedPosition;

            List<int> staffIds = await staffRepository.GetUserStaffIds(model.UserId);
            foreach (int staffId in staffIds)
            {
                foreach (var role in model.CaderRoleIds)
                {
                    if (await userRoleRepository.ExistRoleForUser(model.UserId, role, staffId, model.Id))
                    {
                        return UpdateCaderResult.ExistRoleForUser;
                    }
                }
            }
            #region Update Cader
            cader.Position = model.Position;
            try
            {
                cader.Salary = int.Parse(model.Salary);
            }
            catch
            {
                return UpdateCaderResult.InValidSalary;
            }
            cader.UserId = model.UserId;

            #region Update Cader Roles
            if (model.CaderRoleIds.CheckNullability())
            {
                var list = await userRoleRepository.GetCaderRolesIdentityKeyAsync(model.Id);
                if (list != null)
                {
                    foreach (var role in list)
                    {
                        userRoleRepository.Remove(new UserRole
                        {
                            UserRoleId = role
                        });
                    }
                    await userRoleRepository.SaveChangeAsync();
                }

                #region Add Cader Certificate
                foreach (var role in model.CaderRoleIds)
                {
                    await userRoleRepository.InserAsync(new UserRole
                    {
                        UserId = model.UserId,
                        RoleId = role,
                        CaderId = cader.Id
                    });
                }
                await userRoleRepository.SaveChangeAsync();
                #endregion
            }
            #endregion

            staffRepository.Update(cader);
            await staffRepository.SaveChangeAsync();
            #endregion

            #region Deleteing Cached Cader
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.CaderAboutUsPage);
            #endregion
            return UpdateCaderResult.Success;

        }

        public async Task<UpdateTrainerResult> UpdateTrainerAsync(UpdateTrainerViewModel model)
        {
            var trainer = await staffRepository.GetByIdAsync(model.Id);
            if (trainer == null)
                return UpdateTrainerResult.TrainerNotFound;

            if (await staffRepository.DuplicatedStaffPositionAsync(model.Position, model.UserId, model.Id))
                return UpdateTrainerResult.DuplicatedPosition;

            List<int> staffIds = await staffRepository.GetUserStaffIds(model.UserId);
            foreach (int staffId in staffIds)
            {
                foreach (var certificate in model.TrainerCertificatesIds)
                {
                    if (await userCertificatesRepository.ExistCertificateForUser(model.UserId, certificate, staffId, model.Id))
                    {
                        return UpdateTrainerResult.ExistCertificateForUser;
                    }
                }
            }
            #region Update Trainer
            trainer.Position = model.Position;
            try
            {
                trainer.Salary = int.Parse(model.Salary);
            }
            catch
            {
                return UpdateTrainerResult.InValidSalary;
            }
            trainer.UserId = model.UserId;

            #region Update Trainer Certificates
            if (model.TrainerCertificatesIds.CheckNullability())
            {
                var list = await userCertificatesRepository.GetStaffCertificatesIdentityKeyAsync(model.Id);
                if (list != null)
                {
                    foreach (var certificate in list)
                    {
                        userCertificatesRepository.Remove(new UserCertificates
                        {
                            Id = certificate
                        });
                    }
                    await userCertificatesRepository.SaveChangeAsync();
                }

                #region Add Trainer Certificate
                foreach (var certificate in model.TrainerCertificatesIds)
                {
                    await userCertificatesRepository.InserAsync(new UserCertificates
                    {
                        UserId = model.UserId,
                        CertificateId = certificate,
                        StaffId = trainer.Id
                    });
                }
                await userCertificatesRepository.SaveChangeAsync();
                #endregion
            }
            #endregion

            staffRepository.Update(trainer);
            await staffRepository.SaveChangeAsync();
            #endregion

            #region Deleteing Cached trainer
            string trainerSlug = await userRepository.GetStaffSlugByIdAsync(trainer.UserId);
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.Trainer + trainerSlug);
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.TrainerRelatedKeys);
            #endregion
            return UpdateTrainerResult.Success;

        }

        public async Task<bool> UserHasPermission(int userId)
        => await staffRepository.ExistActiveCaderForUser(userId);

    }
}
