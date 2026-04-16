using BasinTakip.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Configuration
{
    public class DataHistoryConfiguration : EntityTypeConfiguration<DataHistory>
    {
        public DataHistoryConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.EntityId)
                .HasMaxLength(128)
                .IsUnicode();

            Property(p => p.EntityType)
                .HasMaxLength(128)
                .IsUnicode();

            Property(p => p.CreatedMemberId)
                .HasMaxLength(128);

            Property(p => p.UserAgent)
                .HasMaxLength(512);

            Property(p => p.UserHostAddress)
                .HasMaxLength(128);
        }
    }
}
