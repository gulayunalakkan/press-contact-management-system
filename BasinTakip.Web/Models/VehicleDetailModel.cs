using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasinTakip.Web.Models
{
    public class VehicleDetailModel:Vehicle
    {
        public List<PastContactRecordReportModel> ContactRecordBackList { get; set; }
    }
}