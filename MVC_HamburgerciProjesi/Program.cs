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

builder.Services.AddSession(opt => opt.IdleTimeout = TimeSpan.FromSeconds(90));


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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
SeedData.Seed(app);


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Account}/{action=Login}");

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
    pattern: "User/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapAreaControllerRoute(
    name: "UserArea",
    areaName: "User",
    pattern: "User/{controller=Account}/{action=Register}"
    );
    endpoints.MapAreaControllerRoute(
    name: "UserArea",
    areaName: "User",
    pattern: "User/{controller=Siparis}/{action=Index}"
    );
    endpoints.MapAreaControllerRoute(
    name: "AdminArea",
    areaName: "Admin",
    pattern: "{controller=UserManager}/{action=Index}"
    );
    endpoints.MapAreaControllerRoute(
    name: "AdminArea",
    areaName: "Admin",
    pattern: "Admin/{controller=Menu}/{action=Index}"
    );


});



app.Run();
