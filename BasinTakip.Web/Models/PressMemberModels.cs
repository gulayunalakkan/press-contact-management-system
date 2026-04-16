using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Web.Models
{
    public class PressMemberDetailModel: PressMember
    {
        public List<SelectListItem> FirmList { get; set; }
        public List<SelectListItem> TaskList { get; set; }
        public List<SelectListItem> DistrictList { get; set; }
        public List<SelectListItem> GenderList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<PastContactRecordReportModel> ContactRecordBackList { get; set; }
        public HttpPostedFileBase Files { get; set; }
        public string[] SelectedValues { get; set; }
        public IEnumerable<SelectListItem> editionlistValues { get; set; }


    }
}