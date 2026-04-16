using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Web.Models
{
    public class EventDetailModel:Event
    {
        public List<SelectListItem> EventTypeList { get; set; }
        public List<PastContactRecordReportModel> ContactRecordBackList { get; set; }

    }
}