using Autofac.Extensions.DependencyInjection;
using Autofac;
using HamburgerciProject.Application.IoC;
using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Infrastructure.Context;
using HamburgerciProject.Presentation.Models;
using HamburgerciProject.Presentation.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
//    builder.Configuration.GetConnectionString("DefaultConnectionVolkan")));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
   builder.Configuration.GetConnectionString("defaultconnectionfeyza")));

builder.Services.AddSession(opt=>opt.IdleTimeout = TimeSpan.FromSeconds(200));

builder.Services.AddIdentity<AppUser, IdentityRole<int>>
    (
        options => options.SignIn.RequireConfirmedEmail = true
    ).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders().AddErrorDescriber<IdentityValidator>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x=>
    {
        x.LoginPath = "/Account/Login";
    });




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

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


//app.UseAuthorization();
app.UseAuthentication();

SeedData.Seed(app);


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
