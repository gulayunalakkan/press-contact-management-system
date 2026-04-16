using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Repository;
using BasinTakip.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using PagedList;
using System.Collections;
using BasinTakip.Domain.Entities.Abstract;
using System.Data;
using System.Data.SqlTypes;
using BasinTakip.Core.Data;
using System.Data.Entity;
using BasinTakip.Web.Models;

namespace BasinTakip.EntityFramework.Repository
{
    public class EventRepository : GenericRepository<CommonContext, Event, int>, IEventRepository
    {
        public EventRepository(CommonContext context)
            : base(context)
        {
        }

        protected override Expression<Func<Event, bool>> FindyByKeyExpression(int key)
        {
            return p => p.Id == key;
        }

        public IPagedList<EventResult> GetContactPressMemberReport(int pageNumber = 1, int pageSize = 20, DateTime? beginDate = null, string eventPlace = null, int? eventType = null, string orderByColumn = "Id", bool orderType = false, string searchText = null,int? beginyear=null,int?beginmounth=null)
        {
            List<EventResult> result = new List<EventResult>();
            int totalItemCount = 0;
            string searchtext = searchText == null ? searchText : "%" + searchText + "%";

            using (var command = Context.Database.Connection.CreateCommand())
            {
                command.CommandText = "GetContactPressMemberCount";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@PageNumber", pageNumber) ,
                    new SqlParameter("@PageSize", pageSize) ,
                    new SqlParameter("@EventPlace", eventPlace ?? SqlString.Null) ,
                    new SqlParameter("@EventType", eventType ?? SqlInt32.Null),
                    new SqlParameter("@BeginDate", beginDate ?? SqlDateTime.Null) ,
                    new SqlParameter("@OrderByColumn", orderByColumn ?? SqlString.Null) ,
                    new SqlParameter("@OrderType",orderType ),
                    new SqlParameter("@SearchText", searchtext ?? SqlString.Null) ,
                    new SqlParameter("@BeginYear", beginyear ?? SqlInt32.Null),
                    new SqlParameter("@BeginMonth", beginmounth ?? SqlInt32.Null),



                };

                command.Parameters.AddRange(parameters);

                try
                {
                    Context.Database.Connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        result = ((IObjectContextAdapter)Context).ObjectContext
                          .Translate<EventResult>(reader)
                          .ToList();

                        if (reader.NextResult())
                        {
                            reader.Read();

                            totalItemCount = reader.GetInt32(0);
                        }
                    }
                }
                finally
                {
                    Context.Database.Connection.Close();
                }
            }

            return new NoPagedList<EventResult>(result, pageNumber, pageSize, totalItemCount);
        }

        public List<PastContactRecordReportModel> PastContactRecord(int Top)
        {
            List<PastContactRecordReportModel> result = new List<PastContactRecordReportModel>();
            int totalItemCount = 0;

            using (var command = Context.Database.Connection.CreateCommand())
            {
                command.CommandText = "PastContactRecordPressMember";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                DateTime? BackDate = DateTime.Now.AddMonths(-6);
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@BackDate", BackDate ?? SqlDateTime.Null) ,
                    new SqlParameter("@Top",Top) ,
                };

                command.Parameters.AddRange(parameters);

                try
                {
                    Context.Database.Connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        result = ((IObjectContextAdapter)Context).ObjectContext
                          .Translate<PastContactRecordReportModel>(reader)
                          .ToList();

                        if (reader.NextResult())
                        {
                            reader.Read();

                            totalItemCount = reader.GetInt32(0);
                        }
                    }
                }
                finally
                {
                    Context.Database.Connection.Close();
                }
            }

            return result;
        }

        public override Event Save(Event entity)
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

            return entity;
        }
    }
    public class NoPagedList<T> : BasePagedList<T>
    {
        public NoPagedList(IEnumerable<T> subSet, int pageNumber, int pageSize, int totalItemCount)
            : base(pageNumber, pageSize, totalItemCount)
        {
            this.Subset.AddRange(subSet);
        }
    }
}
