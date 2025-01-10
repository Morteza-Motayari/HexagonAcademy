using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Interfaces.Links;
using Hexagon.Domain.Interfaces.Users;
using Hexagon.Domain.Models.Contact_Us;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Domain.ViewModels.Users.Staffs.Caders;
using Hexagon.Infra.Data.Repositories;
using Hexagon.Infra.Data.Repositories.Contact_Us;

namespace Hexagon.Application.Services.Implementation.Users
{
    public class RoleService(IRoleRepository roleRepository,
        IRolePermissionRepository rolePermissionRepository,
        IPermissionRepository permissionRepository,
        IUserRepository userRepository,
        IStaffRepository staffRepository) : IRoleService
    {
        public async Task<AdminSideDetailRoleViewModel?> AdminSideDetailRoleAsync(int roleId)
        {
            var role = await roleRepository.GetByIdAsync(roleId);
            if (role == null)
                return null;
            AdminSideDetailRoleViewModel? Detail = new()
            {
                Id = roleId,
                RoleTitle=role.RoleTitle,
                Permissions=await permissionRepository.GetRolePermissions(roleId),
                caders=await staffRepository.GetCadersWithRoleAsync(roleId),
                IsDeleted = role.IsDeleted,
                CreatedById = role.CreatedBy,
                LastModifiedById = role.LastModifiedBy,
                CreatedDate = role.CreatedDate,
                CreatedBy = await userRepository.GetJustUserName(role.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(role.LastModifiedBy),
                LastModifiedDate = role.LastModifiedDate
            };

            return Detail;
        }

        public async Task<string> CantDeleteRoleForeverNowMessage(int RoleId)
        {
            DateTime lastEdit=await roleRepository.GetLastModifiedDate(RoleId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این نقش تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<CreateRoleResult> CreateRoleAsync(CreateRoleViewModel model)
        {
            if (await roleRepository.ExistRoleTitle(model.RoleTitle))
                return CreateRoleResult.DupliactedRole;
            Role role = new()
            {
                RoleTitle = model.RoleTitle
            };
            await roleRepository.InserAsync(role);
            await roleRepository.SaveChangeAsync();

            #region Add Role Permissions
            if (model.PermissionsId.CheckNullability())
            {
                foreach (var permission in model.PermissionsId)
                {
                    await rolePermissionRepository.InserAsync(new RolePermission { PermissionId = permission, RoleId = role.Id });
                }
                await rolePermissionRepository.SaveChangeAsync();
            }
            #endregion

            return CreateRoleResult.Success;
        }

        public async Task<DeleteRoleResult> DeleteRoleAsync(int RoleId)
        {
            var role = await roleRepository.GetByIdAsync(RoleId);
            if (role == null)
                return DeleteRoleResult.NotFound;
            if(role.IsDeleted==true)
                return DeleteRoleResult.RoleAlreadyDeleted;

            role.IsDeleted = true;
            roleRepository.Update(role);
            await roleRepository.SaveChangeAsync();
            return DeleteRoleResult.Success;
        }

        public async Task<DeleteForeverRoleResult> DeleteRoleForever(int RoleId)
        {
            DateTime lastDate = await roleRepository.GetLastModifiedDate(RoleId);
            if (lastDate.SixMonthPassed())
            {
                var role = await roleRepository.GetByIdAsync(RoleId);
                               
                if (role == null)
                    return DeleteForeverRoleResult.NotFound;

                if (role.IsDeleted == false)
                    return DeleteForeverRoleResult.FirstDeleteSimple;

                roleRepository.Delete(role);
                await roleRepository.SaveChangeAsync();
                return DeleteForeverRoleResult.Success;
            }
            else
            {
                return DeleteForeverRoleResult.CantDeletedNow;
            }
        }

        public async Task<FilterRoleViewModel> FilterRolesAsync(FilterRoleViewModel filter)
        => await roleRepository.FilteRolesAsync(filter);

        public async Task<List<Permission>> GetAllPermmisions()
        => await permissionRepository.GetAllAsync();

        public async Task<UpdateRoleViewModel> GetRoleForEdit(int RoleId)
        {
            var role = await roleRepository.GetByIdAsync(RoleId);
            if (role == null)
                return null;
            return new UpdateRoleViewModel()
            {
                RoleTitle = role.RoleTitle,
                Id = role.Id,
                IsDeleted=role.IsDeleted,
                PermissionsId=await rolePermissionRepository.GetRolePermissionIdsAsync(role.Id)
            };
        }

        public async Task<List<RoleViewModel>> ListRolesAsync()
        => await roleRepository.GetAllRolesOptionAsync();

        public async Task<UpdateRoleResult> UpdateRoleAsync(UpdateRoleViewModel model)
        {
            var role = await roleRepository.GetByIdAsync(model.Id);
            if (role == null)
                return UpdateRoleResult.NotFound;

            if (await roleRepository.ExistRoleTitle(model.RoleTitle,model.Id))
                return UpdateRoleResult.DupliactedRole;

            #region Update Role
            role.RoleTitle = model.RoleTitle;
            roleRepository.Update(role);
            #region Update Role Permissions
            if (model.PermissionsId.CheckNullability())
            {
                var permissions = await rolePermissionRepository.GetRolePermissionsIdentityKeyAsync(model.Id);
                if (permissions != null)
                {
                    foreach (var item in permissions)
                    {
                        rolePermissionRepository.Remove(new RolePermission { Id = item });
                    }
                    await rolePermissionRepository.SaveChangeAsync();
                }
                       
            #region Add new Role Permissions
            foreach (var permission in model.PermissionsId)
            {
                await rolePermissionRepository.InserAsync(new RolePermission { PermissionId = permission, RoleId = role.Id });
            }
            await rolePermissionRepository.SaveChangeAsync();
            }
            #endregion

            #endregion
            await roleRepository.SaveChangeAsync();
            #endregion

            return UpdateRoleResult.Success;
        }
    }
}
