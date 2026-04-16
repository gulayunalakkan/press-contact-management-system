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
    public interface IPersonManager : IGenericManager<IPersonRepository, PressMember, int>
    {
        [HandleError]
        IPagedList<PersonResult> GetInclueded(int pageNumber, int pageSize,DateTime? contactDate
            ,int? editionId,int? taskId,int? cityId,int? districtId=null,bool? block=null, string orderByColumn = "Id", bool orderType = false, string searchText = null, int? BeginYear = null, int? BeginMonth = null);
        [HandleError]
        List<PastContactRecordReportModel> GetFilterContactRecord(int pressMemberId);
        List<PastContactRecordReportModel> GetFilterPressMember(int pressMemberId);
    }
}
