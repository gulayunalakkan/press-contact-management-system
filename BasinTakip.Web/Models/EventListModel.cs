using BasinTakip.Core.Entities.Abstract;
using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Web.Models
{
    public class EventListModel : GenericListInput<Event, int>
    {
        public List<SelectListItem> EventList { get; set; }
        public string EventTypeName { get; set; }
        public int EventTypeId { get; set; }
    }
}