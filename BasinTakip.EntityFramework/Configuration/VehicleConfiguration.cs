using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Configuration
{
   public class VehicleConfiguration : BaseEntityTypeConfiguration<Vehicle, int>
    {
        public VehicleConfiguration()
        {
            Property(p => p.Marka)
                .HasMaxLength(512)
                .IsRequired()
                .IsUnicode();

            Property(p => p.Serial)
               .HasMaxLength(512)
               .IsRequired()
               .IsUnicode();

            Property(p => p.Model)
               .HasMaxLength(512)
               .IsRequired()
               .IsUnicode();

            Property(p => p.Plate)
               .HasMaxLength(512)
               .IsRequired()
               .IsUnicode();

            Property(p => p.ModelDate);

        }
    }
}
