using Hexagon.Application.Generators;
using Hexagon.Application.Security;
using Hexagon.Application.Senders.Implementation;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Account;
using Kavenegar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Services.Implementation.Users
{
    public class AccountService(IUserRepository userRepository) : IAccountService
    {
        public async Task<ForgotPasswordResult> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            User? user = await userRepository.GetByMobileAsync(model.PhoneNumber);

            if (user == null)
                return ForgotPasswordResult.MobileNotfound;

            //To Do: Checking if user is active or not
            string randomCode = CodeGenerator.GenerateCode();
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

        public async Task<LoginResult> loginAsync(LoginViewModel model)
        {
            User? user = await userRepository.GetbyMobileAndPassword(model.PhoneNumber, model.Password);
            if (user == null)
               return LoginResult.UserNotFound;
            if(user.Status==UserStatus.Ban)
                return LoginResult.UserIsBaned;
            if (user.Status == UserStatus.Active)
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
                Password = model.Password.EncodePasswordMd5()
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
    }
}
