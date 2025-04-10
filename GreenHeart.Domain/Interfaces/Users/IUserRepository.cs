using GreenHeart.Domain.Enums.Users;
using GreenHeart.Domain.Models.Records;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Users.Roles;
using GreenHeart.Domain.ViewModels.Users.Staffs.Caders;
using GreenHeart.Domain.ViewModels.Users.Staffs.Trainers;
using GreenHeart.Domain.ViewModels.Users.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<bool> ExistMobileAsync(string mobile);
        Task<bool> ExistMobileAsync(string mobile,int id);
        Task<bool> MobileDuplicatedAsync(string mobile, int userId);
        Task<User?> GetbyMobileAndPassword(string mobile, string password);
        Task<User?> GetByMobileAsync(string mobile);
        Task<User?> GetByMobileAndVerificationCodeAsync(string mobile, string verificationCode);
        Task<FilterUserViewModel> FilteUsersAsync(FilterUserViewModel filter);
        Task<ReadOnlyCollection<UserViewModel>?> GetAllUsersForOptionsAsync(string term);
        Task<ClientSideUpdateUserViewModel?> GetUserForUpdateClientSide(int userId);
        Task<UserClientSideView?> GetUserForViewClientSide(int userId);
        Task<string?> GetJustUserName(int? userId);
        Task<User?> GetUserWithChilds(int userId);
        Task<bool> ExistSpecificSlug(string slug);
        Task<string> PutSpecificSlug(string slug);
        Task<string?> GetJustAvatarAsync(int? userId);
        Task<UserGender?> GetUserGenderAsync(int userId);
        Task<DateTime> GetLastModifiedDate(int id);
        Task<string?> GetUserAvatar(int userId);
        Task<ClientSideFilterTrainerViewModel> ClientSideFilterTrainer(ClientSideFilterTrainerViewModel filter);
        Task<List<ClientSideTrainerViewModel>?> GetTrainersForHomePage();
        Task<User?> GetUserBySlug(string slug);
        Task<List<ClientSideCaderViewModel>?> GetCadersForAbouUsPage();
        Task<string?> GetStaffSlugByIdAsync(int userId);
    }
}
