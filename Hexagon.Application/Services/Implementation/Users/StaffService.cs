using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Interfaces.Links;
using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Staffs.Trainers;
using Hexagon.Infra.Data.Repositories;
using Hexagon.Application.Convertors;

namespace Hexagon.Application.Services.Implementation.Users
{
    public class StaffService(IStaffRepository staffRepository
        , IUserCertificatesRepository userCertificatesRepository
        ,IUserRepository userRepository
        ,ICertificateRepository certificateRepository) : IStaffService
    {
        public async Task<AdminSideDetailTrainerViewModel?> AdminSideDetailTrainerAsync(int TrainerId)
        {
            var trainer = await staffRepository.GetStaffWithUser(TrainerId);
            if (trainer == null)
                return null;
            AdminSideDetailTrainerViewModel? Detail = new()
            {
                Id = TrainerId,
                FullName=trainer.user.GetUserName(),
                Avatar = trainer.user.Avatar,
                Gender = trainer.user.Gender,
                email = trainer.user.email,
                city=trainer.user.city,
                NationalCode = trainer.user.NationalCode,
                PhoneNumber = trainer.user.PhoneNumber,
                Salary=trainer.Salary,
                Position=trainer.Position,
                UserId=trainer.user.Id,
                TrainerCertificates=await certificateRepository.GetTrainerCertificate(trainer.user.Id,TrainerId),
                IsDeleted = trainer.IsDeleted,
                CreatedById = trainer.CreatedBy,
                LastModifiedById = trainer.LastModifiedBy,
                BirthDay = trainer.user.BirthDay?.ToShamsi(),
                CreatedDate = trainer.CreatedDate,
                CreatedBy = await userRepository.GetJustUserName(trainer.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(trainer.LastModifiedBy),
                ModifiedDate = trainer.LastModifiedDate
            };

            return Detail;
        }

        public async Task<CreateTrainerResult> CreateTrainerAsync(CreateTrainerViewModel model)
        {
            var user=await userRepository.GetByIdAsync(model.UserId);
            if (user==null)
                return CreateTrainerResult.UserNotFound;
            if(await staffRepository.DuplicatedStaffPositionAsync(model.Position,model.UserId))
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

            #region Changing User Situation
            user.Situation=UserSituation.Trainer;
            userRepository.Update(user);
            #endregion
            Staff trainer = new()
            {                
                Position = model.Position,
                UserId = model.UserId
            };
            try{
                trainer.Salary = int.Parse(model.Salary);
            }
            catch {
                return CreateTrainerResult.InValidSalary;
            }
            await staffRepository.InserAsync(trainer);
            await staffRepository.SaveChangeAsync();
            if (model.TrainerCertificatesIds.CheckNullability())
            {
                foreach (var certificate in model.TrainerCertificatesIds)
                {
                    await userCertificatesRepository.InserAsync(new UserCertificates
                    {
                        UserId = model.UserId,
                        CertificateId= certificate,
                        StaffId=trainer.Id
                    });
                }
                await userCertificatesRepository.SaveChangeAsync();
            }
            return CreateTrainerResult.Success;
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
            return DeleteTrainerResult.Success;
        }

        public async Task<FilterTrainerViewModel> FilterTrainersAsync(FilterTrainerViewModel filter)
        => await staffRepository.FilteTrainersAsync(filter);

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
                TrainerName= await userRepository.GetJustUserName(trainer.UserId)
            };
        }

        public Task<List<TrainerViewModel>?> ListTrainersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateTrainerResult> UpdateTrainerAsync(UpdateTrainerViewModel model)
        {
            var trainer=await staffRepository.GetByIdAsync(model.Id);
            if(trainer == null)
                return UpdateTrainerResult.TraninerNotFound;

            if (await staffRepository.DuplicatedStaffPositionAsync(model.Position, model.UserId,model.Id))
                return UpdateTrainerResult.DuplicatedPosition;

                List<int> staffIds = await staffRepository.GetUserStaffIds(model.UserId);
                foreach (int staffId in staffIds)
                {
                    foreach (var certificate in model.TrainerCertificatesIds)
                    {
                        if (await userCertificatesRepository.ExistCertificateForUser(model.UserId, certificate, staffId,model.Id))
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

            return UpdateTrainerResult.Success;

        }
    }
}
