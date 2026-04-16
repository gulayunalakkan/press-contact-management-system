using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Abstract
{
    public class PersonResult : PressMember
    {
        public string FirmName { get; set; }
        public string TaskName { get; set; }
        public string EditionName { get; set; }
        public string Adsoyad { get; set; }
        public int? BeginYear { get; set; }
        public int? BeginMonth { get; set; }
        public DateTime? ContactDate { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public string SearchText { get; set; }
        public string orderByColumn { get; set; }
        public bool OrderType { get; set; }
        public string AdSoyadContains { get; set; }

    }
}
