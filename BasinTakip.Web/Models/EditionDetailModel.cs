using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Web.Models
{
    public class EditionDetailModel:Edition
    {
        public List<SelectListItem> EditionTypeList { get; set; }
        public List<PastContactRecordReportModel> EditionWithPressMember { get; set; }
    }
}