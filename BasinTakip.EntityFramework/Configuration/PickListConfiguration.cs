using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Configuration
{
  public class PickListConfiguration : BaseEntityTypeConfiguration<PickList, int>
    {
        public PickListConfiguration()
        {
            Property(p => p.Name)
                .HasMaxLength(512)
                .IsRequired()
                .IsUnicode();

            Property(p => p.CategoryId);
        }
    }
}
