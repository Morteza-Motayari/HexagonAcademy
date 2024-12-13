using Hexagon.Application.Generators;
using Hexagon.Application.Security;
using Hexagon.Application.Senders.Implementation;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Account;
using Hexagon.Domain.ViewModels.Users.Users;
using Kavenegar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagon.Application.Extensions;
using Hexagon.Application.Statics;
using Hexagon.Application.Convertors;
using Hexagon.Infra.Data.Repositories;


namespace Hexagon.Application.Services.Implementation.Users
{
    public class AccountService(IUserRepository userRepository) : IAccountService
    {
        public async Task<ForgotPasswordResult> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            User? user = await userRepository.GetByMobileAsync(model.PhoneNumber);

            if (user == null)
                return ForgotPasswordResult.MobileNotfound;

            //TODO: Checking if user is active or not
            string randomCode = CodeGenerator.GenerateCode();
            //TODO Sending sms
            // var result=smsSender.SendMessage(model.Mobile, $"کد تایید ورود شما {randcode} می باشد.");
            SendResult result = new SendResult();
            result.Status = 200;
            if (result.Status == 200)
            {
                #region Update User
                user.VerificationCode = randomCode;
                userRepository.Update(user);
                await userRepository.SaveChangeAsync();
                #endregion
                return ForgotPasswordResult.Success;
            }
            return ForgotPasswordResult.Error;
        }

        public async Task<User?> GetUserByMobileAsync(string mobile)
        => await userRepository.GetByMobileAsync(mobile);

        public async Task<ClientSideUpdateUserViewModel?> GetUserForUpdateClientSide(int userId)
        {
            User? model = await userRepository.GetByIdAsync(userId);
            if (model == null)
                return null;
            return new ClientSideUpdateUserViewModel
            {
                Id = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDay = model.BirthDay?.ToShamsi(),
                Avatar = model.Avatar,
                Gender = model.Gender,
                email = model.email,
                city = model.city,
                NationalCode = model.NationalCode
            };
        }

        public async Task<LoginResult> loginAsync(LoginViewModel model)
        {
            string hashPassword = model.Password.EncodePasswordMd5();
            User? user = await userRepository.GetbyMobileAndPassword(model.PhoneNumber, hashPassword);
            if (user == null)
               return LoginResult.UserNotFound;
            if(user.Status==UserStatus.Ban)
                return LoginResult.UserIsBaned;
            if (user.Status == UserStatus.NotActive)
                return LoginResult.UserIsNotActive;
            return LoginResult.Success;
        }

        public async Task<RegisterResult> RegisterAsync(RegisterViewModel model)
        {
            if(await userRepository.ExistMobileAsync(model.PhoneNumber)) 
                return RegisterResult.MobileDuplicated;

            User user = new()
            {
                PhoneNumber = model.PhoneNumber,
                Password = model.Password.EncodePasswordMd5(),
                Status=UserStatus.Active,
                Situation=UserSituation.Athlete
            };
            await userRepository.InserAsync(user);
            await userRepository.SaveChangeAsync();
            return RegisterResult.Success;
        }

        public async Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            User? user=await userRepository.GetByMobileAndVerificationCodeAsync(model.PhoneNumber,model.VerificationCode);
            if(user == null)
                return ResetPasswordResult.WrongCode;

            #region Update User
            user.VerificationCode = null;
            user.Password = model.NewPassword.EncodePasswordMd5();
            userRepository.Update(user);
            await userRepository.SaveChangeAsync();
            #endregion

            return ResetPasswordResult.Success;
        }

        public async Task<ClientSideUpdateUserResult> UpdateUserClientSideAsync(ClientSideUpdateUserViewModel model)
        {
            User? user = await userRepository.GetByIdAsync(model.Id);
            if(user == null)
                return ClientSideUpdateUserResult.UserNotFound;
            #region Update User Info
            user.Gender = model.Gender;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            if(model.BirthDay != null)
            {
                if (!model.BirthDay.CheckPersianDate())
                {
                    return ClientSideUpdateUserResult.InvalidDateTime;
                }
                user.BirthDay = model.BirthDay.ToMiladi();
            }
            user.NationalCode = model.NationalCode;
            user.email=model.email;
            user.city = model.city;
            #region Update Trainer Slug 
            if (user.Situation == UserSituation.Trainer)
            {
                if (user.FirstName != model.FirstName || user.LastName != model.LastName)
                {
                    string slug = model.GetUserName().GenerateSlug();
                    if (await userRepository.ExistSpecificSlug(slug))
                    {
                        slug = await userRepository.PutSpecificSlug(slug);
                    }
                    user.Slug = slug;
                }
            }
            #endregion
            #region Adding Avatar
            if (model.NewImage != null)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewImage.FileName);
                model.NewImage.AddImageToServer(imageName, SavingPath.AvatarPath);
                user.Avatar = imageName;
            }
            #endregion
            userRepository.Update(user);
            await userRepository.SaveChangeAsync();
            #endregion

            return ClientSideUpdateUserResult.Success;
        }
    }
}
