using HamburgerciProject.Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Infrastructure.Mapping
{
    public class SiparisMapping : IEntityTypeConfiguration<Siparis>
    {
        public void Configure(EntityTypeBuilder<Siparis> builder)
        {
            builder.HasOne(s => s.User)
                .WithMany(u => u.Siparisler)
                .HasForeignKey(u => u.AppUserId);

            builder.HasMany(s=>s.Menuler)
                .WithOne(m=>m.Siparis)
                .HasForeignKey(m=>m.SiparisId);

            builder.HasMany(s => s.EkstraMalzemeleri)
           .WithOne(e => e.Siparis)
           .HasForeignKey(e => e.SiparisId);

        }



    }
}
