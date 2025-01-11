using Hexagon.Application.Statics;
using Hexagon.Infra.Data.Context;
using Hexagon.Infra.IOC.Container;
using Hexagon.MVC.Utilities.ActionFilters;
using Microsoft.AspNetCore.Authentication.Cookies;
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

    //#region Action Filter
    //builder.Services.AddControllers(options =>
    //{
    //    options.Filters.Add<UserInfoFilter>();
    //});
    //#endregion

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(LogLevel.Trace);
    builder.Host.UseNLog();

    #region HtmlEncoder
    //this code is for using persian fonts in some components like sweet alert 
    builder.Services.AddSingleton<HtmlEncoder>(
    HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
UnicodeRanges.Arabic }));
    #endregion

    #region register Sevices
    builder.Services.RegisterServices();

    builder.Services.AddHttpClient();
    builder.Services.AddScoped<UserInfoFilter>();
    #endregion


    System.Net.ServicePointManager.SecurityProtocol =
    System.Net.SecurityProtocolType.Tls12 |
    System.Net.SecurityProtocolType.Tls13;

    #region Configure Hexagon Context
    var connectionString = builder.Configuration.GetConnectionString("AcademyConnectionStrings");
    builder.Services.AddDbContext<HexagonContext>(options =>
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

    #region Adding Session Support
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(20);
        options.Cookie.HttpOnly = true;
    });
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
