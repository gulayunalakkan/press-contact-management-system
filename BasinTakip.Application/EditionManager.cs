using BasinTakip.Core.Business;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Manager;
using BasinTakip.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasinTakip.Domain.Entities.Abstract;
using PagedList;
using BasinTakip.Core.Data;
using BasinTakip.Web.Models;

namespace BasinTakip.Application
{
    public class EditionManager : GenericManager<IEditionRepository, Edition, int>, IEditionManager
    {
        public List<PastContactRecordReportModel> GetFilterEditionWithPress(int EditionId)
        {
            using (IocManager.BeginScope())
            {
                var personRepository = IocManager.Resolve<IPersonRepository>();
                var contactRecordRepository = IocManager.Resolve<IContactRecordRepository>();
                var PickListlcvRepository = IocManager.Resolve<IPickListRepository>();
                var PickListPartRepository = IocManager.Resolve<IPickListRepository>();
                var EditionRepository = IocManager.Resolve<IEditionRepository>();

                var query = from person in personRepository.All()
                            where person.EditionIds.Contains(EditionId.ToString())
                            select new PastContactRecordReportModel
                            {
                                firstNamelastName = person.FirstName == null || person.LastName == null ? "-" : person.FirstName + " " + person.LastName,
                                Email = person.Email,
                                MobilePhone=person.MobilePhone,
                                Id = person.Id,
                            };
                

                return query.ToList();
            }
        }

        public IPagedList<EditionResult> GetInclueded(int pageNumber, int pageSize, string orderByColumn = "Id", bool orderType = false, string searchText = null,int? EditionId=null,int? EditionTypeId=null)
        {
            using (IocManager.BeginScope())
            {
                var EditionRepository = IocManager.Resolve<IEditionRepository>();
                var PickListRepository = IocManager.Resolve<IPickListRepository>();
                var result = EditionRepository.All();
                if (EditionId.HasValue)
                {
                     result = EditionRepository.Filter(x => x.Id == EditionId);
                }
                if (EditionTypeId.HasValue)
                {
                    result = result.Where(x => x.EditionTypeId == EditionTypeId);
                }
                var query = from edition in result
                            join PickList in PickListRepository.All() on edition.EditionTypeId equals PickList.Id into pcl
                            from pcls in pcl.DefaultIfEmpty()
                            orderby edition.Id
                            select new EditionResult
                            {
                                Id = edition.Id,
                                Name = edition.Name == null ? "-" : edition.Name,
                                EditionTypeName = pcls.Name == null ? " " : pcls.Name,
                                Adress = edition.Adress == null ? "-" : edition.Adress
                            };
                if (!string.IsNullOrEmpty(searchText))
                {
                    //var entityType = typeof(Edition).Name;
                    //var searchTableRepository = IocManager.Resolve<ISearchTableRepository>();
                    //var fullTextSearchText = searchText;// FtsInterceptor.Fts(searchText);

                    //query = from search in searchTableRepository.All()
                    //        join Edition in query on search.EntityId equals Edition.Id.ToString()
                    //        where search.EntityType == entityType && search.SearchData.Contains(fullTextSearchText)
                    //        select Edition;

                  query=  query.Where(x => x.Name.Contains(searchText));
                }

                if (orderType)
                {
                    query = query.OrderByDescending(orderByColumn);
                }
                else
                {
                    query = query.OrderBy(orderByColumn);
                }

                return query.ToPagedList(pageNumber, pageSize);
            }
        }


    }
}
