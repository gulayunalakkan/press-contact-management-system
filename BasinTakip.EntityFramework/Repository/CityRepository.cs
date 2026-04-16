using BasinTakip.Domain.Entities.Base;
using BasinTakip.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Repository
{
   public class CityRepository:IDisposable
    {

        public CityRepository()
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
        protected DbSet<City> DbSet { get; set; }

        public virtual IEnumerable<City> All()
        {
            return Context.Set<City>().ToList();
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
