using BasinTakip.Core.Data;
using BasinTakip.Core.Entities.Base;
using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Repository
{
    public interface ISearchTableRepository: IRepository
    {
        IQueryable<SearchTable> All();
        void Delete(SearchTable entity);
        SearchTable Save(SearchTable entity);
    }
}
