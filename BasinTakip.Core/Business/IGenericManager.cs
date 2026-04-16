using BasinTakip.Core.Data;
using BasinTakip.Core.Dependecy;
using BasinTakip.Entities.Base;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Core.Business
{
    public interface IGenericManager<TRepository, TEntity, TKey> : IManager
               where TEntity : EntityBase<TKey>
               where TRepository : IGenericRepository<TEntity, TKey>
    {
        [HandleError]
        TEntity GetByKey(TKey key);
        [HandleError]
        List<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);
        [HandleError]
        IPagedList<TEntity> FilterPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
        [HandleError]
        List<TEntity> All();
        [HandleError]
        IPagedList<TEntity> AllPaged(int pageNumber, int pageSize);
        [HandleError]
        void DeleteByKey(TKey key);
        [HandleError]
        void Delete(TEntity entity);
        [HandleError]
        TEntity Save(TEntity entity);
    }
}
