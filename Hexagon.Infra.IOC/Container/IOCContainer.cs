using Hexagon.Application.Senders.Implementation;
using Hexagon.Application.Senders.Interfaces;
using Hexagon.Application.Services.Implementation.Contact_Us;
using Hexagon.Application.Services.Implementation.Gyms;
using Hexagon.Application.Services.Implementation.KeyWords;
using Hexagon.Application.Services.Implementation.Users;
using Hexagon.Application.Services.Interfaces.Contact_Us;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Application.Services.Interfaces.KeyWords;
using Hexagon.Application.Services.Interfaces.Records;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Interfaces.Contact_Us;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Interfaces.KeyWords;
using Hexagon.Domain.Interfaces.Links;
using Hexagon.Domain.Interfaces.Users;
using Hexagon.Infra.Data.Repositories;
using Hexagon.Infra.Data.Repositories.Contact_Us;
using Hexagon.Infra.Data.Repositories.Gyms;
using Hexagon.Infra.Data.Repositories.KeyWords;
using Hexagon.Infra.Data.Repositories.Links;
using Hexagon.Infra.Data.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IEmailSender, EmailSender>();
            #endregion

            #region Contact Us
            services.AddScoped<IContactUsService, ContactUsService>();
            #endregion

            #region Gyms
            services.AddScoped<IGymService, GymService>();
            services.AddScoped<IGymGalleryService, GymGalleryService>();
            services.AddScoped<ISportService, SportService>();
            services.AddScoped<ISportClassService, SportClassService>();
            services.AddScoped<IClassCommentService, ClassCommentService>();
            services.AddScoped<IClassCommentReactionService, ClassCommentReactionService>();
            #endregion

            #region Key Words
            services.AddScoped<IKeyWordService, KeyWordService>();
            #endregion

            #region Records
            services.AddScoped<ICertificateService, CertificateService>();
            services.AddScoped<IExperienceService, ExperienceService>();
            #endregion

            #endregion

            #region Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #region Contact Us
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            #endregion

            #region Gyms
            services.AddScoped<IGymRepository, GymRepository>();
            services.AddScoped<IGymGalleryRepository, GymGalleryRepository>();
            services.AddScoped<ISportRepository, SportRepository>();
            services.AddScoped<ISportClassRepository, SportClassRepository>();
            services.AddScoped<ISportClassRepository, SportClassRepository>();
            services.AddScoped<IClassCommentRepository, ClassCommentRepository>();
            services.AddScoped<IClassCommentReactionRepository, ClassCommentReactionRepository>();
            #endregion

            #region Key Words
            services.AddScoped<IKeyWordRepository, KeyWordRepository>();
            #endregion

            #region Links
            services.AddScoped<IClassUserRepository, ClassUserRepository>();
            services.AddScoped<IGymUserRepository, GymUserRepository>();
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
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRolePermissionRepository,RolePermissionRepository>();
            services.AddScoped<IPermissionRepository,PermissionRepository>();
            #endregion

            #endregion
        }
    }
}
