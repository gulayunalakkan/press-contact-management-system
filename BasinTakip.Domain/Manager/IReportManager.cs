using BasinTakip.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Manager
{
  public interface IReportManager
    {
        List<PastContactRecordReportModel> Backreport();
        List<PastContactRecordReportModel> LastContactReport();
        List<PastContactRecordReportModel> ContactReport();
    }
}
