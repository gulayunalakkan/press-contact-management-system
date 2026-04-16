using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Configuration
{
    public class FirmConfiguration : BaseEntityTypeConfiguration<Firm, int>
    {
        public FirmConfiguration()
        {
            Property(p => p.Name)
                .HasMaxLength(512)
                .IsRequired()
                .IsUnicode();
        }
    }
}
