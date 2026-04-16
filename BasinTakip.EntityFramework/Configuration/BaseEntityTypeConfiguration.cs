using BasinTakip.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Configuration
{
    public class BaseEntityTypeConfiguration<TEntityType, TKey> : EntityTypeConfiguration<TEntityType> 
        where TEntityType : EntityBase<TKey>
    {
        public BaseEntityTypeConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.Permalink)
                .HasMaxLength(512)
                .IsRequired()
                .IsUnicode();

            Property(p => p.CreatedMemberId)
                .HasMaxLength(128)
                .IsRequired();

            Property(p => p.ModifiedMemberId)
                .HasMaxLength(128)
                .IsRequired();

            Property(p => p.DeletedMemberId)
                .HasMaxLength(128);
        }
    }
}
