using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Configuration
{
   public class PickListCategoryConfiguration : BaseEntityTypeConfiguration<PickListCategory, int>
    {
        public PickListCategoryConfiguration()
        {
            Property(p => p.Name)
                .HasMaxLength(512)
                .IsRequired()
                .IsUnicode();

        }
    }
}
