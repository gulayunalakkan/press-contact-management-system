using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Configuration
{
    public class PressMemberConfiguration : BaseEntityTypeConfiguration<PressMember, int>
    {
        public PressMemberConfiguration()
        {
            Property(p => p.FirstName)
                .HasMaxLength(512)
                .IsRequired()
                .IsUnicode();

            Property(p => p.LastName)
               .HasMaxLength(512)
               .IsRequired()
               .IsUnicode();

            Property(p => p.About)
                .HasMaxLength(4096)
                .IsUnicode();

            Property(p => p.Adress)
                .HasMaxLength(4096);

            Property(p => p.MobilePhone)
                .IsRequired()
               .HasMaxLength(4096)
               .IsUnicode(); ;

            Property(p => p.Fax)
               .HasMaxLength(4096);

            Property(p => p.Email)
             .HasMaxLength(4096)
             .IsUnicode();

            Property(p => p.Email2)
             .HasMaxLength(4096)
             .IsUnicode();
            
            Property(p => p.BirthDate);

            Property(p => p.Block);
            Property(p => p.EditionId);
            Property(p => p.TaskId);
            Property(p => p.DistrictId);
        }
    }
}
