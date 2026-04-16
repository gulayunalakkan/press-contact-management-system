using BasinTakip.EntityFramework.Context;
using BasinTakip.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.EntityFramework.Repository
{
    public class ReportRepository : IDisposable
    {
        public ReportRepository()
        {
            _CommonContext = new CommonContext();

        }
        private CommonContext _CommonContext = null;
        protected CommonContext Context
        {

            get { return _CommonContext; }
            set { _CommonContext = value; }
        }

        private bool disposed = false;
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

        public List<PastContactRecordReportModel> GetEventReport(int pageNumber = 1, int pageSize = 20,int Take=int.MaxValue)
        {
            List<PastContactRecordReportModel> result = new List<PastContactRecordReportModel>();
            using (var command = Context.Database.Connection.CreateCommand())
            {
                command.CommandText = "GetEventReport";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@PageNumber", pageNumber),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@Take",Take)
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
