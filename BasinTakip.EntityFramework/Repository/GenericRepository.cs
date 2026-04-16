using BasinTakip.Core.Data;
using BasinTakip.Core.Dependecy;
using BasinTakip.Core.Entities.Base;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Repository;
using BasinTakip.Entities.Base;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Repository
{
    public abstract class GenericRepository<TContext, TEntity, TKey> :
       IGenericRepository<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TContext : DbContext, new()
    {
        public GenericRepository(TContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        private TContext _context = null;
        protected TContext Context
        {

            get { return _context; }
            set { _context = value; }
        }

        protected DbSet<TEntity> DbSet { get; set; }

        protected abstract Expression<Func<TEntity, bool>> FindyByKeyExpression(TKey key);

        public virtual TEntity GetByKey(TKey key)
        {
            var entity = All().FirstOrDefault(FindyByKeyExpression(key));

            return entity;
        }
   

        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            var results = All()
                .Where(predicate);

            return results;
        }
        public virtual IPagedList<TEntity> FilterPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize)
        {
            var results = Filter(predicate)
                    .OrderBy(p => p.Id)
                    .ToPagedList(pageNumber, pageSize);

            return results;
        }

        public virtual IQueryable<TEntity> All()
        {
            return DbSet
                    .Where(p => !p.IsDeleted);
        }

        public virtual IPagedList<TEntity> AllPaged(int pageNumber, int pageSize)
        {
            var results = All()
                    .OrderBy(p => p.Id)
                    .ToPagedList(pageNumber, pageSize);

            return results;
        }

        public virtual void DeleteByKey(TKey key)
        {
            var entity = DbSet.FirstOrDefault(FindyByKeyExpression(key));

            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            Context.SaveChanges();
        }

        public virtual TEntity Save(TEntity entity)
        {
            bool entityExistsInDb = DbSet.Any(FindyByKeyExpression(entity.Id));

            if (entityExistsInDb)
            {
                if (!DbSet.Local.Any(p => p == entity))
                {
                    DbSet.Attach(entity);
                }

                Context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                DbSet.Add(entity);
            }

            Context.SaveChanges();

            GenerateSearchTable(entity);

            return entity;
        }

        public virtual void GenerateSearchTable(TEntity entity)
        {
            using (IocManager.BeginScope())
            {
                var searchTableRepository = IocManager.Resolve<ISearchTableRepository>();

                string entityId = entity.Id.ToString();
                string entityType = entity.GetType().Name;
                SearchTable mySearchTable = null;

                mySearchTable = searchTableRepository.All().FirstOrDefault(p => p.EntityId == entityId
                                                              && p.EntityType == entityType);

                if (entity == null ? false : entity.IsDeleted)
                {
                    searchTableRepository.Delete(mySearchTable);
                }
                else
                {
                    if (mySearchTable == null)
                    {
                        mySearchTable = new SearchTable
                        {
                            EntityId = entityId,
                            EntityType = entityType
                        };
                    }
                    mySearchTable.SearchData = GetSearchData(entity);
                    searchTableRepository.Save(mySearchTable);

                }
            }
        }

        public virtual string GetSearchData(TEntity entity)
        {
            string result = string.Empty;

            var stringProperties = typeof(TEntity).GetProperties()
                .Where(p => p.PropertyType == typeof(string));

            foreach (var item in stringProperties)
            {
                result += (item.GetValue(entity) as string) ?? string.Empty;
                result += " ";
            }

            return result;
        }
    }
}
