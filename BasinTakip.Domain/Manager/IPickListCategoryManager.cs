using BasinTakip.Core.Business;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using System.Linq.Expressions;

namespace BasinTakip.Domain.Manager
{
    public interface IPickListCategoryManager : IGenericManager<IPickListCategoryRepository, PickListCategory, int>
    {

    }
}
