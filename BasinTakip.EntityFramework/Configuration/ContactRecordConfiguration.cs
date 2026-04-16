using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Configuration
{
   public class ContactRecordConfiguration : BaseEntityTypeConfiguration<ContactRecord, int>
    {
        public ContactRecordConfiguration()
        {
            Property(p => p.Notes)
                .HasMaxLength(4096)
                .IsUnicode();
        }
    }
}
