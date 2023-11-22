using HamburgerciProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using HamburgerciProject.Domain.Entities.Concrete;
using MVC_HamburgerciProjesi.Models.Enum;
using HamburgerciProject.Domain.Enum;
using System.Net.Mime;
using HamburgerciProject.Application.Models.VMs;

namespace HamburgerciProject.Presentation.SeedData
{
    public static class SeedData
    {
        public static async void Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                //Guid guidKey = Guid.NewGuid();
                //string guidKey = Guid.NewGuid().ToString();
                AppDbContext context = scope.ServiceProvider.GetService<AppDbContext>();
                context.Database.Migrate();

                if (!context.EkstraMalzemeler.Any())
                {
                    context.EkstraMalzemeler.AddRange(
                        new EkstraMalzeme()
                        {
                            EkstraAdi = "Range Sos",
                            EkstraFiyat = 6,
                            CreateDate = DateTime.Now
                        },
                        new EkstraMalzeme()
                        {
                            EkstraAdi = "Ketçap",
                            EkstraFiyat = 3,
                            CreateDate = DateTime.Now,
                        },
                        new EkstraMalzeme()
                        {
                            EkstraAdi = "Mayonez",
                            EkstraFiyat = 3,
                            CreateDate = DateTime.Now,
                        },
                        new EkstraMalzeme()
                        {
                            EkstraAdi = "Hardal",
                            EkstraFiyat = 3,
                            CreateDate = DateTime.Now,
                        }
                        );
                    await context.SaveChangesAsync();
                }

                if (!context.Menuler.Any())
                {
                    context.Menuler.AddRange(
                        new Menu()
                        {
                            MenuAdi = "SteakHouse Menü",
                            MenuFiyati = 50,
                            Status = Status.Active,
                            CreateDate = DateTime.Now,
                        },
                        new Menu()
                        {
                            MenuAdi = "BigKing Menü",
                            MenuFiyati = 70,
                            Status = Status.Active,
                            CreateDate = DateTime.Now,
                        },
                        new Menu()
                        {
                            MenuAdi = "Double King Chicken Menü",
                            MenuFiyati = 70,
                            Status = Status.Active,
                            CreateDate = DateTime.Now,
                        },
                        new Menu()
                        {
                            MenuAdi = "Wooper Menü",
                            MenuFiyati = 60,
                            Status = Status.Active,
                            CreateDate = DateTime.Now,
                        },
                        new Menu()
                        {
                            MenuAdi = "Wooper Jr. Menü",
                            MenuFiyati = 50,
                            Status = Status.Active,
                            CreateDate = DateTime.Now,
                        },
                        new Menu()
                        {
                            MenuAdi = "Chicken Royal Menü",
                            MenuFiyati = 70,
                            Status = Status.Active,
                            CreateDate = DateTime.Now,
                        }
                        );
                    await context.SaveChangesAsync();
                }


                if (!context.EkstraMalzemeler.Any())
                {
                    context.EkstraMalzemeler.AddRange(
                        new EkstraMalzeme()
                        {
                            EkstraAdi = "Range Sos",
                            EkstraFiyat = 6,
                            CreateDate = DateTime.Now
                        },
                        new EkstraMalzeme()
                        {
                            EkstraAdi = "Ketçap",
                            EkstraFiyat = 3,
                            CreateDate = DateTime.Now,
                        },
                        new EkstraMalzeme()
                        {
                            EkstraAdi = "Mayonez",
                            EkstraFiyat = 3,
                            CreateDate = DateTime.Now,
                        },
                        new EkstraMalzeme()
                        {
                            EkstraAdi = "Hardal",
                            EkstraFiyat = 3,
                            CreateDate = DateTime.Now,
                        }
                        );
                    await context.SaveChangesAsync();
                }

                if (!context.Menuler.Any())
                {
                    context.Menuler.AddRange(
                        new Menu()
                        {
                            MenuAdi = "SteakHouse Menü",
                            MenuFiyati = 50,
                            Status = Status.Active,
                            CreateDate = DateTime.Now,
                        },
                        new Menu()
                        {
                            MenuAdi = "BigKing Menü",
                            MenuFiyati = 70,
                            Status = Status.Active,
                            CreateDate = DateTime.Now,
                        },
                        new Menu()
                        {
                            MenuAdi = "Double King Chicken Menü",
                            MenuFiyati = 70,
                            Status = Status.Active,
                            CreateDate = DateTime.Now,
                        },
                        new Menu()
                        {
                            MenuAdi = "Wooper Menü",
                            MenuFiyati = 60,
                            Status = Status.Active,
                            CreateDate = DateTime.Now,
                        },
                        new Menu()
                        {
                            MenuAdi = "Wooper Jr. Menü",
                            MenuFiyati = 50,
                            Status = Status.Active,
                            CreateDate = DateTime.Now,
                        },
                        new Menu()
                        {
                            MenuAdi = "Chicken Royal Menü",
                            MenuFiyati = 70,
                            Status = Status.Active,
                            CreateDate = DateTime.Now,
                        }
                        );
                    await context.SaveChangesAsync();
                }

                if (!context.Siparisler.Any())
                {
                    Siparis siparis = new()
                    {
                        Adedi = 1,
                        CreateDate = DateTime.Now,
                        Status = Status.Active,
                        Menuler = new HashSet<MenuSiparis>()
                        {
                            new MenuSiparis()
                            {
                                MenuId = 1,
                            },
                            new MenuSiparis()
                            {
                                MenuId = 2,
                            },
                            new MenuSiparis()
                            {
                                MenuId = 3,
                            },
                        },
                        EkstraMalzemeler = new HashSet<EkstraMalzemelerSiparis>()
                        {
                            new EkstraMalzemelerSiparis()
                            {
                                EkstraMalzemeId = 1,
                            },
                            new EkstraMalzemelerSiparis()
                            {
                                EkstraMalzemeId = 2,
                            }
                        },
                        appUser = new()
                        {
                            CreateDate = DateTime.Now,
                            UserName = "volkan",
                            Status = Status.Active,
                            Password = "123",
                            Email = "volkan@hotmail.com",
                            UserRole = "admin"
                        }
                    };

                    await context.AddAsync(siparis);
                    await context.SaveChangesAsync();



                    //         );
                    if (!context.Users.Any())
                    {
                        context.Users.Add(new AppUser() { UserName = "volkan", Email = "volkancavusoglu@hotmail.com", Password = "123", UserRole = "admin" });
                        //}
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
