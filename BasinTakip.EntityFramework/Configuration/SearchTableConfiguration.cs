using BasinTakip.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Configuration
{
    public class SearchTableConfiguration: EntityTypeConfiguration<SearchTable>
    {
        public SearchTableConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.EntityId)
                .HasMaxLength(128)
                .IsUnicode();

            Property(p => p.EntityType)
                .HasMaxLength(128)
                .IsUnicode();
        }
    }
}
