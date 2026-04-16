using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Base
{
   public class PressMemberLicense
    {
        public int PressMemberId { get; set; }
        public int LicenseId { get; set; }

        public PressMember Person { get; set; }
        public PickList License { get; set; }

    }
}
