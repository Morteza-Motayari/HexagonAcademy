using GreenHeart.Application.Senders.Implementation;
using GreenHeart.Application.Senders.Interfaces;
using GreenHeart.Application.Services.Implementation.Banners;
using GreenHeart.Application.Services.Implementation.Caching;
using GreenHeart.Application.Services.Implementation.Contact_Us;
using GreenHeart.Application.Services.Implementation.Gyms;
using GreenHeart.Application.Services.Implementation.KeyWords;
using GreenHeart.Application.Services.Implementation.Orders;
using GreenHeart.Application.Services.Implementation.Payment;
using GreenHeart.Application.Services.Implementation.Tickets;
using GreenHeart.Application.Services.Implementation.Users;
using GreenHeart.Application.Services.Implementation.Wallets;
using GreenHeart.Application.Services.Interfaces.Banners;
using GreenHeart.Application.Services.Interfaces.Caching;
using GreenHeart.Application.Services.Interfaces.Contact_Us;
using GreenHeart.Application.Services.Interfaces.Gyms;
using GreenHeart.Application.Services.Interfaces.KeyWords;
using GreenHeart.Application.Services.Interfaces.Orders;
using GreenHeart.Application.Services.Interfaces.Payment;
using GreenHeart.Application.Services.Interfaces.Records;
using GreenHeart.Application.Services.Interfaces.Tickets;
using GreenHeart.Application.Services.Interfaces.Users;
using GreenHeart.Application.Services.Interfaces.Wallets;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Interfaces.Banners;
using GreenHeart.Domain.Interfaces.Contact_Us;
using GreenHeart.Domain.Interfaces.Gyms;
using GreenHeart.Domain.Interfaces.KeyWords;
using GreenHeart.Domain.Interfaces.Links;
using GreenHeart.Domain.Interfaces.Orders;
using GreenHeart.Domain.Interfaces.Tickets;
using GreenHeart.Domain.Interfaces.Users;
using GreenHeart.Domain.Interfaces.Wallets;
using GreenHeart.Infra.Data.Repositories;
using GreenHeart.Infra.Data.Repositories.Banners;
using GreenHeart.Infra.Data.Repositories.Contact_Us;
using GreenHeart.Infra.Data.Repositories.Gyms;
using GreenHeart.Infra.Data.Repositories.KeyWords;
using GreenHeart.Infra.Data.Repositories.Links;
using GreenHeart.Infra.Data.Repositories.Orders;
using GreenHeart.Infra.Data.Repositories.Tickets;
using GreenHeart.Infra.Data.Repositories.Users;
using GreenHeart.Infra.Data.Repositories.Wallets;
using Microsoft.Extensions.DependencyInjection;

namespace GreenHeart.Infra.IOC.Container
{
    public static class IOCContainer
    {
        public static void RegisterServices (this IServiceCollection services)
        {
            #region Services

            #region Banners
            services.AddScoped<IBannerService, BannerService>();
            #endregion

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

            #region Caching
            services.AddScoped<ICacheService, RedisCacheService>();
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

            #region Orders
            services.AddScoped<IOrderService, OrderService>();
            #endregion

            #region Payment
            services.AddScoped<INovinoService, NovinoService>();
            #endregion

            #region Records
            services.AddScoped<ICertificateService, CertificateService>();
            services.AddScoped<IExperienceService, ExperienceService>();
            #endregion

            #region Tickets
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketMessageService, TicketMessageService>();
            #endregion

            #region Wallets
            services.AddScoped<IWalletService, WalletService>();
            #endregion

            #endregion

            #region Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #region Banners
            services.AddScoped<IBannerRepository, BannerRepository>();
            #endregion

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

            #region Orders
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IClassOrderRepository, ClassOrderRepository>();
            #endregion

            #region Records
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<IExperienceRepository, ExperienceRepository>();
            #endregion

            #region Tickets
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketMessageRepository, TicketMessageRepository>();
            #endregion

            #region Users
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRolePermissionRepository,RolePermissionRepository>();
            services.AddScoped<IPermissionRepository,PermissionRepository>();
            #endregion

            #region Wallets
            services.AddScoped<IWalletRepository, WalletRepository>();
            #endregion

            #endregion
        }
    }
}
