using BasinTakip.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasinTakip.Core.Entities.Base;
using System.Data.Entity;
using BasinTakip.EntityFramework.Context;
using BasinTakip.Core.Data;

namespace BasinTakip.EntityFramework.Repository
{
    public class DataHistoryRepository : IDataHistoryRepository
    {
        public DataHistoryRepository(CommonContext context)
        {
            _context = context;
            DbSet = context.Set<DataHistory>();
        }

        private CommonContext _context = null;
        protected CommonContext Context
        {

            get { return _context; }
            set { _context = value; }
        }

        protected DbSet<DataHistory> DbSet { get; set; }

        public DataHistory Save(DataHistory entity)
        {
            DbSet.Add(entity);

            Context.SaveChanges();

            return entity;
        }
    }
}
