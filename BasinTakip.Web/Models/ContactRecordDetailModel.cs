using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Web.Models
{
    public class ContactRecordDetailModel:ContactRecord
    {
        public List<SelectListItem> EditionTypeList { get; set; }
        public List<SelectListItem> PressMemberList { get; set; }
        public List<SelectListItem> ContactTypeList { get; set; }
        public List<SelectListItem> ContactTypeSubCategoryList { get; set; }
        public List<SelectListItem> LCVList { get; set; }
        public List<SelectListItem> participationStatusList { get; set; }
        public List<SelectListItem> ContactKindList { get; set; }

    }
}