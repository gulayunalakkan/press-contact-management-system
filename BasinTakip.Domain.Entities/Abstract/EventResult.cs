using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Abstract
{
    public class EventResult : Event
    {
        public string EventTypeName { get; set; }
        public int contactPressSum { get; set; }
        public int Count { get; set; }
    }
}
