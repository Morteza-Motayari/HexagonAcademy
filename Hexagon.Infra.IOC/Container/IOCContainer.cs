using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Interfaces.Links;
using Hexagon.Domain.Interfaces;
using Hexagon.Infra.Data.Repositories.Gyms;
using Hexagon.Infra.Data.Repositories.Links;
using Hexagon.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Application.Services.Implementation.Users;
using Hexagon.Application.Senders.Interfaces;
using Hexagon.Application.Senders.Implementation;
using Hexagon.Domain.Interfaces.Users;
using Hexagon.Infra.Data.Repositories.Users;

namespace Hexagon.Infra.IOC.Container
{
    public static class IOCContainer
    {
        public static void RegisterServices (this IServiceCollection services)
        {
            #region Services

            #region Users
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISmsSender, SmsSender>();
            services.AddScoped<IRoleService, RoleService>();

            #endregion

            #endregion

            #region Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #region Gyms
            services.AddScoped<IGymRepository, GymRepository>();
            services.AddScoped<IGymGalleryRepository, GymGalleryRepository>();
            services.AddScoped<ISportRepository, SportRepository>();
            services.AddScoped<ISportClassRepository, SportClassRepository>();
            #endregion

            #region Links
            services.AddScoped<IClassUserRepository, ClassUserRepository>();
            services.AddScoped<IGymUsersRepository, GymUsersRepository>();
            services.AddScoped<IUserCertificatesRepository, UserCertificatesRepository>();
            #endregion

            #region Records
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<IExperienceRepository, ExperienceRepository>();
            #endregion

            #region Users
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            #endregion

            #endregion
        }
    }
}
