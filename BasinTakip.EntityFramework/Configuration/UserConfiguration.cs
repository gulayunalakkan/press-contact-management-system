using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using BasinTakip.Domain.Entities.Base;

namespace BasinTakip.EntityFramework.Configuration
{
    internal class UserConfiguration:EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(p => p.UserName).IsRequired()
                .HasMaxLength(100);
            Property(p => p.PasswordHash).IsRequired()
                .HasMaxLength(256);
            HasIndex(p => p.UserName).IsUnique();
        }       

    }
}
