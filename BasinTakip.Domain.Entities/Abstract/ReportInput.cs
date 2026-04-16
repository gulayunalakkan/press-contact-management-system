using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Abstract
{
   public class ReportInput
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchText { get; set; }
        public int? BeginYear { get; set; }
        public int? BeginMonth { get; set; }
        public int? EndYear { get; set; }
        public int? Endmonth { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string EventPlace { get; set; }
        public int? EventType { get; set; }
        public DateTime? ContactDate { get; set; }
        public int? EditionId { get; set; }
        public int? TaskId { get; set; }
        public int? ContactType { get; set; }
        public int? Edition { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public bool? Block { get; set; }
        public string OrderByColumn { get; set; }
        public bool OrderType { get; set; }
    }
}
