using BasinTakip.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasinTakip.Core.Entities.Base;
using System.Data.Entity;
using BasinTakip.EntityFramework.Context;

namespace BasinTakip.EntityFramework.Repository
{
    public class SearchTableRepository : ISearchTableRepository
    {
        public SearchTableRepository(CommonContext context)
        {
            _context = context;
            DbSet = context.Set<SearchTable>();
        }

        private CommonContext _context = null;
        protected CommonContext Context
        {

            get { return _context; }
            set { _context = value; }
        }

        protected DbSet<SearchTable> DbSet { get; set; }

        public IQueryable<SearchTable> All()
        {
            return DbSet;
        }

        public void Delete(SearchTable entity)
        {
            DbSet.Remove(entity);
            Context.SaveChanges();
        }

        public SearchTable Save(SearchTable entity)
        {
            //string entityId = entity.Id.ToString();
            //string entityType = entity.GetType().Name;

            bool entityExistsInDb = DbSet.Any(p => p.EntityId == entity.EntityId
                                                        && p.EntityType == entity.EntityType);

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

            return entity;
        }
    }
}
