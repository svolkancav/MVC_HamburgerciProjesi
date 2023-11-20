using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Repositories;
using HamburgerciProject.Infrastructure.Context;
using HamburgerciProject.Infrastructure.Repositories;
using HamburgerciProject.Presentation.Models;
using HamburgerciProject.Presentation.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnectionVolkan")));

builder.Services.AddSession(opt => opt.IdleTimeout = TimeSpan.FromSeconds(90));

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>

    (
    options => options.SignIn.RequireConfirmedAccount = true
    ).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddTransient<IMenuService, MenuService>();
builder.Services.AddTransient<IAppUserService, AppUserService>();
builder.Services.AddTransient<IEkstraMalzemeService, EkstraMalzemeService>();
builder.Services.AddTransient<ISiparisService, SiparisService>();



builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<IMenuRepository, MenuRepository>();
builder.Services.AddTransient<IEkstraMalzemeRepository, EkstraMalzemeRepository>();
builder.Services.AddTransient<ISiparisRepository, SiparisRepository>();
builder.Services.AddTransient<IAppUserRepository, AppUserRepository>();
builder.Services.AddTransient<IAppUserService, AppUserService >();


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "UserArea",
    areaName: "User",
    pattern: "{UserArea}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapAreaControllerRoute(
    name: "AdminArea",
    areaName: "Admin",
    pattern: "{AdminArea}/{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
