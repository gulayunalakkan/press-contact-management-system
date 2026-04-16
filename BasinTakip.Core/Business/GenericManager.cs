using BasinTakip.Core.Business;
using BasinTakip.Core.Data;
using BasinTakip.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using PagedList;

namespace BasinTakip.Core.Business
{
    public abstract class GenericManager<TRepository, TEntity, TKey> : IGenericManager<TRepository, TEntity, TKey>
               where TEntity : EntityBase<TKey>
               where TRepository : IGenericRepository<TEntity, TKey>
    {
        public virtual List<TEntity> All()
        {
            using (IocManager.BeginScope())
            {
                var repository = IocManager.Resolve<TRepository>();
                return repository
                            .All()
                            .ToList();
            }
        }

        public virtual IPagedList<TEntity> AllPaged(int pageNumber, int pageSize)
        {
            using (IocManager.BeginScope())
            {
                var repository = IocManager.Resolve<TRepository>();

                return repository
                            .AllPaged(pageNumber, pageSize);
            }
        }

        public virtual void DeleteByKey(TKey key)
        {
            using (IocManager.BeginScope())
            {
                var repository = IocManager.Resolve<TRepository>();

                var entity = repository.GetByKey(key);

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.Now;
                repository.Save(entity);
            }
        }
        public virtual void Delete(TEntity entity)
        {
            using (IocManager.BeginScope())
            {
                var repository = IocManager.Resolve<TRepository>();

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.Now;

                repository.Save(entity);
            }
        }

        public virtual List<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            using (IocManager.BeginScope())
            {
                var repository = IocManager.Resolve<TRepository>();

                return repository
                            .Filter(predicate)
                            .ToList();
            }
        }

        public virtual IPagedList<TEntity> FilterPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize)
        {
            using (IocManager.BeginScope())
            {
                var repository = IocManager.Resolve<TRepository>();

                return repository
                            .FilterPaged(predicate, pageNumber, pageSize);
            }
        }

        public virtual TEntity GetByKey(TKey key)
        {
            using (IocManager.BeginScope())
            {
                var repository = IocManager.Resolve<TRepository>();

                return repository.GetByKey(key);
            }
        }

        public virtual TEntity Save(TEntity entity)
        {
            using (IocManager.BeginScope())
            {
                var repository = IocManager.Resolve<TRepository>();

                entity.ModifiedAt = DateTime.Now;
                entity.CreatedAt = DateTime.Now;

                if (string.IsNullOrEmpty(entity.CreatedMemberId))
                {
                    entity.CreatedMemberId = string.Empty;

                }
                if (string.IsNullOrEmpty(entity.ModifiedMemberId))
                {
                    entity.ModifiedMemberId = string.Empty;
                }

                if (string.IsNullOrEmpty(entity.Permalink))
                {
                    entity.Permalink = Guid.NewGuid()
                                            .ToString()
                                            .Replace("-", string.Empty)
                                            .ToLower();
                }

                return repository.Save(entity);
            }
        }


    }
}
