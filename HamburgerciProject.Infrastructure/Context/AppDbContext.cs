using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Infrastructure.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Infrastructure.Context
{
    public class AppDbContext: IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<EkstraMalzeme> EkstraMalzemeler { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<Menu> Menuler { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Sepet> Sepetler { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            new MenuSiparisMapping().Configure(builder.Entity<MenuSiparis>());
            new EkstraMalzemelerSiparisMapping().Configure(builder.Entity<EkstraMalzemelerSiparis>());

            //mapping
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.uselazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
