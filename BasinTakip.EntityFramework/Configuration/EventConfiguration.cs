using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Configuration
{
   public class EventConfiguration : BaseEntityTypeConfiguration<Event, int>
    {
        public EventConfiguration()
        {
            Property(p => p.Name)
                .HasMaxLength(512)
                .IsRequired()
                .IsUnicode();

            Property(p => p.BeginDate)
               .IsRequired();

            Property(p => p.EndDate)
               .IsRequired();

            Property(p => p.EventPlace)
               .HasMaxLength(4096)
               .IsUnicode();

            Property(p => p.EventAdress)
               .HasMaxLength(4096)
               .IsUnicode();

            Property(p => p.EventTypeId)
               .IsRequired();
        }
    }
}
