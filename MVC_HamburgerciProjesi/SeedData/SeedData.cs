using HamburgerciProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using HamburgerciProject.Domain.Entities.Concrete;
using MVC_HamburgerciProjesi.Models.Enum;
using HamburgerciProject.Domain.Enum;

namespace HamburgerciProject.Presentation.SeedData
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                Guid guidKey = Guid.NewGuid();
                //string guidKey = Guid.NewGuid().ToString();
                AppDbContext context = scope.ServiceProvider.GetService<AppDbContext>();
                context.Database.Migrate();
                context.Users.Add(new AppUser() { UserName = "volkan", Email = "volkancavusoglu@hotmail.com", Password = "123", UserRole = "admin" });
                if (!context.Siparişler.Any())
                {
                    context.Siparişler.AddRange(
                        new Siparis()
                        {
                           Adedi = 1,
                           CreateDate = DateTime.Now,
                           EkstraMalzemeleri =
                           {
                               new EkstraMalzeme()
                               {
                                   EkstraAdi ="Range Sos",
                                   EkstraFiyat = 6,
                                   CreateDate = DateTime.Now
                               },
                               new EkstraMalzeme()
                               {
                                   EkstraAdi ="Ketçap",
                                   EkstraFiyat = 3,
                                   CreateDate = DateTime.Now,
                               },
                               new EkstraMalzeme()
                               {
                                   EkstraAdi ="Mayonez",
                                   EkstraFiyat = 3,
                                   CreateDate = DateTime.Now,
                               },
                               new EkstraMalzeme()
                               {
                                   EkstraAdi ="Hardal",
                                   EkstraFiyat = 3,
                                   CreateDate = DateTime.Now,
                               },

                           },
                           Menuler = new List<Menu> 
                           { 
                               new Menu()
                               {
                                   MenuAdi = "SteakHouse Menü",
                                   MenuFiyati = 50,
                                   Boyutu = Boyut.Büyük,
                                   Status = Status.Active,
                                   CreateDate= DateTime.Now,
                               },
                               new Menu()
                               {
                                   MenuAdi = "BigKing Menü",
                                   MenuFiyati = 70,
                                   Boyutu = Boyut.Büyük,
                                   Status = Status.Active,
                                   CreateDate= DateTime.Now,
                               },
                               new Menu()
                               {
                                   MenuAdi = "Double King Chicken Menü",
                                   MenuFiyati = 70,
                                   Boyutu = Boyut.Büyük,
                                   Status = Status.Active,
                                   CreateDate= DateTime.Now,
                               },
                               new Menu()
                               {
                                   MenuAdi = "Wooper Menü",
                                   MenuFiyati = 60,
                                   Boyutu = Boyut.Büyük,
                                   Status = Status.Active,
                                   CreateDate= DateTime.Now,
                               },
                               new Menu()
                               {
                                   MenuAdi = "Wooper Jr. Menü",
                                   MenuFiyati = 50,
                                   Boyutu = Boyut.Küçük,
                                   Status = Status.Active,
                                   CreateDate= DateTime.Now,
                               },
                               new Menu()
                               {
                                   MenuAdi = "Chicken Royal Menü",
                                   MenuFiyati = 70,
                                   Boyutu = Boyut.Orta,
                                   Status = Status.Active,
                                   CreateDate= DateTime.Now,
                               }

                           },


                        }

                         );
                    
                }
                context.SaveChanges();
            }
        }
    }
}
