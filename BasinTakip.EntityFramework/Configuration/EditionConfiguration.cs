using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Configuration
{
  public class EditionConfiguration : BaseEntityTypeConfiguration<Edition, int>
    {
        public EditionConfiguration()
        {
            Property(p => p.Name)
                .HasMaxLength(512)
                .IsRequired()
                .IsUnicode();

            Property(p => p.Adress)
               .HasMaxLength(4096)
               .IsUnicode();

            Property(p => p.EditionTypeId)
                .IsRequired();

        }
    }
}
