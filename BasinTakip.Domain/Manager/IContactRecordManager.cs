using BasinTakip.Core.Business;
using BasinTakip.Core.Dependecy;
using BasinTakip.Domain.Entities.Abstract;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Manager
{
   public interface IContactRecordManager : IGenericManager<IContactRecordRepository, ContactRecord, int>
    {
        [HandleError]
        IPagedList<ContactRecordResult> GetInclueded(int pageNumber, int pageSize,DateTime? beginDate,int? editionId,
            int? ContactTypeId, string orderByColumn = "Id", bool orderType = false, string searchText = null,int? beginyear=null,int? beginmounth=null);
    }
}
