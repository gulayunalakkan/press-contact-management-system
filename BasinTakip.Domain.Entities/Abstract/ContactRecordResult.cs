using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Abstract
{
   public class ContactRecordResult:ContactRecord
    {
        public string EditionTypeName { get; set; }
        public string PressMemberName { get; set; }
        public string ContactTypeName { get; set; }  // temas şekli üst kategori
        public string ContactTypeSubName { get; set; }  // temas şekli alt kategori
        public string LcvName { get; set; }
        public string participationStatusName { get; set; }
        public string CreateDate { get; set; }
        public string filterNotes { get; set; }
        public DateTime BeginDate { get; set; }
        public string orderByColumn { get; set; }
        public bool OrderType { get; set; }
        public string SearchText { get; set; }
        public int? BeginYear { get; set; }
        public int? BeginMonth { get; set; }
        public int? EditionId { get; set; }
        public int? ContactTypeId { get; set; }
    }
}
