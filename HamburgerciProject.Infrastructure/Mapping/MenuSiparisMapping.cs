using HamburgerciProject.Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Infrastructure.Mapping
{
    public class MenuSiparisMapping : IEntityTypeConfiguration<MenuSiparis>
    {
        public void Configure(EntityTypeBuilder<MenuSiparis> builder)
        {
            builder.HasKey(s => new { s.SiparisId, s.MenuId });

            builder.HasOne(s => s.siparis).WithMany(m => m.Menuler).HasForeignKey(f=>f.SiparisId); 

            builder.HasOne(s => s.menu).WithMany(m => m.Siparisler).HasForeignKey(f=>f.MenuId);

        }



    }
}
