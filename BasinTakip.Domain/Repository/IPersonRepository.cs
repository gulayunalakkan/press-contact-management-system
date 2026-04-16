using BasinTakip.Core.Data;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Repository
{
    public interface IPersonRepository : IGenericRepository<PressMember, int>
    {
        List<PastContactRecordReportModel> PressMemberDetail(int id);
    }
}
