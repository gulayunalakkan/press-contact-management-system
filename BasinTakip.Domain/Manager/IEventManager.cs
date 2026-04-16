using BasinTakip.Core.Business;
using BasinTakip.Core.Dependecy;
using BasinTakip.Domain.Entities.Abstract;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Repository;
using BasinTakip.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Manager
{
    public interface IEventManager : IGenericManager<IEventRepository, Event, int>
    {
        [HandleError]
        IPagedList<EventResult> GetInclueded(int pageNumber, int pageSize, DateTime? beginDate, string eventPlace, int? eventType, string orderByColumn = "Id", bool orderType = false, string searchText = null,int? beginyear=null,int? beginmounth=null);
        [HandleError]
        List<PastContactRecordReportModel> GetFilterContactRecord(int eventId);
    }
}
