using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Interfaces.Users;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Services.Implementation.Users
{
    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        public async Task<CreateRoleResult> CreateRoleAsync(CreateRoleViewModel model)
        {
            if(await roleRepository.ExistRoleTitle(model.RoleTitle))
                return CreateRoleResult.DupliactedRole;
            Role role = new()
            {
                RoleTitle = model.RoleTitle,
                RoleName = model.RoleName,
                CreatedDate = DateTime.Now
            };
            await roleRepository.InserAsync(role);
            await roleRepository.SaveChangeAsync();
            return CreateRoleResult.Success;
        }

        public async Task<DeleteRoleResult> DeleteRoleAsync(int RoleId)
        {
            var role=await roleRepository.GetByIdAsync(RoleId);
            if (role == null)
                return DeleteRoleResult.NotFound;
            role.IsDeleted = true;
            await roleRepository.SaveChangeAsync();
            return DeleteRoleResult.Success;
        }

        public async Task<FilterRoleViewModel> FilterRolesAsync(FilterRoleViewModel filter)
        => await roleRepository.FilteRolesAsync(filter);

        public async Task<UpdateRoleViewModel> GetRoleForEdit(int RoleId)
        {
            var role = await roleRepository.GetByIdAsync(RoleId);
            if (role == null)
                return null;
            return new UpdateRoleViewModel()
            {
                RoleTitle = role.RoleTitle,
                RoleName = role.RoleName,
                Id = role.Id,
            };
        }

        public async Task<List<RoleViewModel>> ListRolesAsync()
        => await roleRepository.GetAllRolesAsync();

        public async Task<UpdateRoleResult> UpdateRoleAsync(UpdateRoleViewModel model)
        {
            var role = await roleRepository.GetByIdAsync(model.Id);
            if (role == null)
                return UpdateRoleResult.NotFound;

            if (await roleRepository.ExistRoleTitle(model.RoleTitle))
                return UpdateRoleResult.DupliactedRole;

            #region Update Role
            role.RoleTitle = model.RoleTitle;
            role.RoleName = model.RoleName;
            roleRepository.Update(role);
            await roleRepository.SaveChangeAsync();
            #endregion

            return UpdateRoleResult.Success;
        }
    }
}
