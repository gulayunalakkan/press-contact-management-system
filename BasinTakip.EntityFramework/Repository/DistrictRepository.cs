using BasinTakip.Domain.Entities.Base;
using BasinTakip.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Repository
{
   public class DistrictRepository: IDisposable
    {

        public DistrictRepository()
        {
            _context = new CommonContext();
        }

        private CommonContext _context = null;
        protected CommonContext Context
        {

            get { return _context; }
            set { _context = value; }
        }
        private bool disposed = false;
        protected DbSet<District> DbSet { get; set; }

        public virtual IEnumerable<District> All()
        {
            return Context.Set<District>().ToList();
        }
        public virtual IQueryable<District> Filter(Expression<Func<District, bool>> predicate)
        {
            var results = Context.Set<District>()
                .Where(predicate);

            return results;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    Context.Dispose();

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
