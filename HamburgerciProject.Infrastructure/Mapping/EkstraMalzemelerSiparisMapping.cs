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
    internal class EkstraMalzemelerSiparisMapping:IEntityTypeConfiguration<EkstraMalzemelerSiparis>
    {
        public void Configure(EntityTypeBuilder<EkstraMalzemelerSiparis> builder)
        {

            builder.HasKey(s => new { s.SiparisId, s.EkstraMalzemeId });

            builder.HasOne(s => s.siparis).WithMany(m => m.EkstraMalzemeler).HasForeignKey(f => f.SiparisId);

            builder.HasOne(s => s.ekstraMalzeme).WithMany(m => m.Siparisler).HasForeignKey(f => f.EkstraMalzemeId);

        }
    }
}
