using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Configuration
{
   public class PressMemberLicenseConfiguration:EntityTypeConfiguration<PressMemberLicense>
    {
        public PressMemberLicenseConfiguration()
        {
            HasKey(p => new { p.LicenseId, p.PressMemberId });

            Property(p => p.PressMemberId);
            Property(p => p.LicenseId);


        }
    }
}
