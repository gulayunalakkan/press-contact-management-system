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
   public interface IEditionManager: IGenericManager<IEditionRepository, Edition, int>
    {
        [HandleError]
        IPagedList<EditionResult> GetInclueded(int pageNumber, int pageSize, string orderByColumn = "Id", bool orderType = false, string searchText = null,int? EditionId=null,int? EditionTypeId=null);
        List<PastContactRecordReportModel> GetFilterEditionWithPress(int EditionId);

    }
}
