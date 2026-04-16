using BasinTakip.Domain.Entities.Base;
using BasinTakip.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Repository
{
    public class EventCalendarRepository : IDisposable
    {

        public EventCalendarRepository()
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
        protected DbSet<EventSpecial> DbSet { get; set; }

        public virtual IEnumerable<EventSpecial> All()
        {
            return Context.Set<EventSpecial>().ToList();
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

        public List<EventSpecial> GetEventCalendarList()
        {
            List<EventSpecial> result = new List<EventSpecial>();

            using (var command = Context.Database.Connection.CreateCommand())
            {
                command.CommandText = "GetEventCalendar";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    Context.Database.Connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        result = ((IObjectContextAdapter)Context).ObjectContext
                          .Translate<EventSpecial>(reader)
                          .ToList();
                    }
                }
                finally
                {
                    Context.Database.Connection.Close();
                }
            }

            return result;
        }


    }
}
