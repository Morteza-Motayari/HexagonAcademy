using EasyCaching.Core.Configurations;
using GreenHeart.Application.Statics;
using GreenHeart.Application.Statics.WebSite_Texts;
using GreenHeart.Infra.Data.Context;
using GreenHeart.Infra.IOC.Container;
using GreenHeart.MVC.Utilities.ActionFilters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NLog.Web;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;


var logger=NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(LogLevel.Trace);
    builder.Host.UseNLog();

    #region HtmlEncoder
    //this code is for using persian fonts in some components like sweet alert 
    builder.Services.AddSingleton<HtmlEncoder>(
    HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
UnicodeRanges.Arabic }));
    #endregion

    #region Redis Caching
    //use redis cache that named redis1
    builder.Services.AddEasyCaching(options =>
    {
        // Use JSON serializer
        options.WithJson("json");

        options.UseRedis(config =>
        {
            config.DBConfig.Endpoints.Add(new ServerEndPoint("localhost", 6379));
            config.DBConfig.ConnectionTimeout = 10000; // Increase timeout to 10 seconds
            config.SerializerName = "json"; // Match the serializer name
        }, "redis1");
    });

    #endregion

    #region register Sevices
    builder.Services.RegisterServices();

    builder.Services.AddHttpClient();
    builder.Services.AddScoped<UserInfoFilter>();
    #endregion

    #region Set Point Manager for Payment
    System.Net.ServicePointManager.SecurityProtocol =
    System.Net.SecurityProtocolType.Tls12 |
    System.Net.SecurityProtocolType.Tls13;
    #endregion

    #region Configure GreenHeart Context
    var connectionString = builder.Configuration.GetConnectionString("AcademyConnectionStrings");
    builder.Services.AddDbContext<GreenHeartContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });
    #endregion

    #region Authentication
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LogoutPath = "/logOut";
            options.LoginPath = "/Login";
            options.ExpireTimeSpan = TimeSpan.FromDays(30);
        });
    #endregion

    #region Config kaveNegar
    builder.Configuration.GetSection("KaveNegarInfo").Get<KaveNegarStatics>();
    builder.Configuration.GetSection("MelipayamakInfo").Get<MelipayamakStatics>();
    #endregion

    #region HtppContextAccessor
    builder.Services.AddHttpContextAccessor();
    #endregion

    #region Add Json Serialization
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
    #endregion

    #region Configure Cookie
    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    });
    #endregion

    #region Adding Session Support
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(20);
        options.Cookie.HttpOnly = true;
    });
    #endregion

    #region Configure Appsetting Texts
    builder.Services.AddSingleton(builder.Configuration.GetSection("Footer").Get<Footer>());
    builder.Services.AddSingleton(builder.Configuration.GetSection("HomeBoby").Get<HomeBoby>());
    builder.Services.AddSingleton(builder.Configuration.GetSection("SportsList").Get<SportsList>());
    #endregion



    #region Configuring DataProtection for machine key

    //Configure Data Protection

    //var dirInfo = new DirectoryInfo(@"C:\Inetpub\vhosts\greenheartgym.com\keys");
    //Console.WriteLine($"Can Read: {dirInfo.Exists}");

    //var dataProtectionBuilder = builder.Services.AddDataProtection()
    //    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\Inetpub\vhosts\greenheartgym.com\keys"))
    //    .SetApplicationName("greenheartgym");


    #endregion

    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseStatusCodePagesWithReExecute("/Error/{0}");

    app.UseHttpsRedirection();


    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSession();

    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

}
catch (Exception? exception)
{
    logger.Error(exception,"Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
