using BasinTakip.Core.Entities.Abstract;
using BasinTakip.Domain.Entities.Abstract;
using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Web.Models
{
    public class ContactRecordListModel:ContactRecord
    {
        public List<SelectListItem> EditionList { get; set; }
    }
}