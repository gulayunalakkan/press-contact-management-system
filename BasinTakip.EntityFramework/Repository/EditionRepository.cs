using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Repository;
using BasinTakip.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using BasinTakip.Web.Models;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Entity.Infrastructure;

namespace BasinTakip.EntityFramework.Repository
{
   public class EditionRepository : GenericRepository<CommonContext, Edition, int>, IEditionRepository
    {
        public EditionRepository(CommonContext context)
            : base(context)
        {
        }

        protected override Expression<Func<Edition, bool>> FindyByKeyExpression(int key)
        {
            return p => p.Id == key;
        }

        public List<PastContactRecordReportModel> PastContactListEditionWithPress(int EditionId)
        {
            List<PastContactRecordReportModel> result = new List<PastContactRecordReportModel>();
            int totalItemCount = 0;

            using (var command = Context.Database.Connection.CreateCommand())
            {
                command.CommandText = "GetPressListWithEditionId";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@EditionId",EditionId) ,
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

        public List<PastContactRecordReportModel> PastContactEditionWithPress(int EditionId)
        {
            List<PastContactRecordReportModel> result = new List<PastContactRecordReportModel>();
            int totalItemCount = 0;

            using (var command = Context.Database.Connection.CreateCommand())
            {
                command.CommandText = "GetPressWithEditionId"; 
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@EditionId",EditionId) ,
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

        public override string GetSearchData(Edition entity)
        { string Result=base.GetSearchData(entity);
            var PickList= IocManager.Resolve<IPickListRepository>();
            Result += string.Join(
                "", from p in PickList.All()
                    where p.CategoryId == 4 && p.Id == entity.EditionTypeId
                    select p.Name);
            return Result;
        }
    }
}
