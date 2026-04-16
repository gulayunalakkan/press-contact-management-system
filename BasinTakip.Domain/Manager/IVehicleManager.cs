using BasinTakip.Core.Business;
using BasinTakip.Core.Dependecy;
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
   public interface IVehicleManager : IGenericManager<IVehicleRepository, Vehicle, int>
    {
        [HandleError]
        IPagedList<Vehicle> GetInclueded(int pageNumber, int pageSize,string orderByColumn = "Id", bool orderType = true, string searchText = null,string ModelDate=null);
        [HandleError]
        List<PastContactRecordReportModel> GetFilterContactRecord(int VehicleId);
    }
}
