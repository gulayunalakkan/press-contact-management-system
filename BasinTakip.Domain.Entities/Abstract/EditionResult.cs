using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Abstract
{
   public class EditionResult:Edition
    {
        public string EditionTypeName { get; set; }
        public string OrderByColumn { get; set; }
        public bool OrderType { get; set; }
        public string SearchText { get; set; }

    }
}
