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

            builder.HasMany(s => s.Menuler)
                .WithMany(m => m.Siparisler);

            builder.HasMany(s => s.EkstraMalzemeleri)
           .WithMany(e => e.Siparisler);

            builder.HasOne(s => s.AppUser)
                .WithMany(a => a.Siparişler)
                .HasForeignKey(a => a.AppUserId);


        }



    }
}
