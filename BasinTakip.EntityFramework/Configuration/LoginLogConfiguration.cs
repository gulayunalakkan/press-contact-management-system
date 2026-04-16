using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Configuration
{
  public class LoginLogConfiguration : EntityTypeConfiguration<LoginLog>
    {
        public LoginLogConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.UserName)
                .HasMaxLength(512)
                .IsRequired()
                .IsUnicode();

        }
    }
}
