using Hexagon.Application.Statics;
using Hexagon.Infra.Data.Context;
using Hexagon.Infra.IOC.Container;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


#region HtmlEncoder
//this code is for using persian fonts in some components like sweet alert 
builder.Services.AddSingleton<HtmlEncoder>(
HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
UnicodeRanges.Arabic }));
#endregion

#region register Sevices
builder.Services.RegisterServices();
#endregion

builder.Services.AddControllersWithViews();

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
        options.ExpireTimeSpan=TimeSpan.FromDays(30);
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

var app = builder.Build();





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
