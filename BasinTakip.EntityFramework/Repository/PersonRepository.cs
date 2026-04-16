using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Repository;
using BasinTakip.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using BasinTakip.Web.Models;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlTypes;

namespace BasinTakip.EntityFramework.Repository
{
    public class PersonRepository : GenericRepository<CommonContext, PressMember, int>, IPersonRepository
    {
        public PersonRepository(CommonContext context) : base(context)
        {
        }

        protected override Expression<Func<PressMember, bool>> FindyByKeyExpression(int key)
        {
            return p => p.Id == key;
        }

        public override string GetSearchData(PressMember entity)
        {
            string result = base.GetSearchData(entity);

            var pickListRepository = IocManager.Resolve<IPickListRepository>();
            var editionRepository = IocManager.Resolve<IEditionRepository>();
            var taskName = string.Join("", from p in pickListRepository.All() where p.CategoryId==1 && p.Id==entity.TaskId
                                           select p.Name);
            var editionName = string.Join(" ", from edition in editionRepository.All()
                                               where edition.Id == entity.EditionId
                                               select edition.Name);

            result += " " + (taskName ?? string.Empty) + " " + (editionName ?? string.Empty);

            return result;
        }

        public List<PastContactRecordReportModel> PressMemberDetail(int id)
        {
            List<PastContactRecordReportModel> result = new List<PastContactRecordReportModel>();
            int totalItemCount = 0;

            using (var command = Context.Database.Connection.CreateCommand())
            {
                command.CommandText = "PressMemberDetail";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", id) ,
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

    }
}
