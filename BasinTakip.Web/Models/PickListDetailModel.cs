using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Web.Models
{
    public class PickListDetailModel:PickList
    {
        public List<SelectListItem> PicklistList { get; set; }
        public List<SelectListItem> PickListCategoryList { get; set; }
    }
}