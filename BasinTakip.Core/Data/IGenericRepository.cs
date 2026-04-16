using BasinTakip.Core.Dependecy;
using BasinTakip.Entities.Base;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Core.Data
{
    public interface IGenericRepository<TEntity, TKey> : IRepository
            where TEntity : class, IEntity<TKey>
    {
        TEntity GetByKey(TKey key);
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);
        IPagedList<TEntity> FilterPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
        IQueryable<TEntity> All();
        IPagedList<TEntity> AllPaged(int pageNumber, int pageSize);
        void DeleteByKey(TKey key);
        void Delete(TEntity entity);
        [DataHistory]
        TEntity Save(TEntity entity);
    }
}
