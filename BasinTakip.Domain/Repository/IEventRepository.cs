using BasinTakip.Core.Data;
using BasinTakip.Domain.Entities.Abstract;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Repository
{
   public interface IEventRepository:IGenericRepository<Event,int>
    {
        IPagedList<EventResult> GetContactPressMemberReport(int pageNumber = 1, int pageSize = 20, DateTime? beginDate=null, string eventPlace=null, int? eventType=null, string orderByColumn = "Id", bool orderType = false, string searchText = null,int?beginyear=null,int?beginmounth=null);
        List<PastContactRecordReportModel> PastContactRecord(int Top);
    }
}
