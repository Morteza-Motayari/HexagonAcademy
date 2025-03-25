using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Account;
using GreenHeart.Domain.ViewModels.Users.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Interfaces.Users
{
    public interface IAccountService
    {
        Task<RegisterResult> RegisterAsync(RegisterViewModel model);
        Task<LoginResult> loginAsync(LoginViewModel model);
        Task<ForgotPasswordResult> ForgotPasswordAsync(ForgotPasswordViewModel model);
        Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<User?> GetUserByMobileAsync(string mobile);
        Task<ClientSideUpdateUserViewModel?> GetUserForUpdateClientSide(int userId);
        Task<ClientSideUpdateUserResult> UpdateUserClientSideAsync(ClientSideUpdateUserViewModel model);
    }
}
