using Autofac.Extensions.DependencyInjection;
using Autofac;
using HamburgerciProject.Application.IoC;
using HamburgerciProject.Application.Services.AppUserService;
using HamburgerciProject.Application.Services.EkstraMalzemeServices;
using HamburgerciProject.Application.Services.MenuServices;
using HamburgerciProject.Application.Services.SiparisServices;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Repositories;
using HamburgerciProject.Infrastructure.Context;
using HamburgerciProject.Infrastructure.Repositories;
using HamburgerciProject.Presentation.Models;
using HamburgerciProject.Presentation.SeedData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Autofac.Core;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
//    builder.Configuration.GetConnectionString("DefaultConnectionVolkan")));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
   builder.Configuration.GetConnectionString("defaultconnectionfeyza")));


//builder.Services.AddSession(options =>
//{
//	options.Cookie.Name = ".MVC_HamburgerciProjesi.Session";
//	options.IdleTimeout = TimeSpan.FromMinutes(5);
//	options.Cookie.HttpOnly = true;
//	options.Cookie.IsEssential = true;
//});




//Cookie ler için eklendi. Deneme
//builder.Services
//    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//      .RequireAuthenticatedUser()
//      .Build();
//});


builder.Services.AddIdentity<AppUser, IdentityRole<int>>
    (
    options => options.SignIn.RequireConfirmedEmail = true
    ).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders().AddErrorDescriber<IdentityValidator>();



builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new DependencyResolver());
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
SeedData.Seed(app);


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
endpoints.MapAreaControllerRoute(
   name: "UserArea",
   areaName: "User",
   pattern: "{controller=Account}/{action=Login}"
   );
endpoints.MapAreaControllerRoute(
name: "UserArea",
areaName: "User",
pattern: "{controller=Home}/{action=Index}/{id?}"
);
endpoints.MapAreaControllerRoute(
name: "UserArea",
areaName: "User",
pattern: "{controller=Account}/{action=Register}"
);
endpoints.MapAreaControllerRoute(
name: "UserArea",
areaName: "User",
pattern: "{controller=Siparis}/{action=Index}"
);
endpoints.MapAreaControllerRoute(
name: "AdminArea",
areaName: "Admin",
pattern: "{controller=UserManager}/{action=Index}"
);
endpoints.MapAreaControllerRoute(
name: "AdminArea",
areaName: "Admin",
pattern: "{controller=Menu}/{action=Index}"
);

});



app.Run();
