using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Services.Interfaces.Users
{
    public interface IAccountService
    {
        Task<RegisterResult> RegisterAsync(RegisterViewModel model);
        Task<LoginResult> loginAsync(LoginViewModel model);
        Task<ForgotPasswordResult> ForgotPasswordAsync(ForgotPasswordViewModel model);
        Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<User?> GetUserByMobileAsync(string mobile);
    }
}
