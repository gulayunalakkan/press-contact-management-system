using BasinTakip.Core.Business;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Manager;
using BasinTakip.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using BasinTakip.Core.Data;
using BasinTakip.Web.Models;

namespace BasinTakip.Application
{
    public class VehicleManager : GenericManager<IVehicleRepository, Vehicle, int>, IVehicleManager
    {
        public IPagedList<Vehicle> GetInclueded(int pageNumber, int pageSize, string orderByColumn = "Id", bool orderType = false, string searchText = null,string ModelYear=null)
        {
            using (IocManager.BeginScope())
            {
                
                var vehicleRepository = IocManager.Resolve<IVehicleRepository>();
                var query = vehicleRepository.All();
                if (ModelYear != null)
                {
                    query = query.Where(x => x.ModelDate == ModelYear && x.IsDeleted==false);
                }
                if (!string.IsNullOrEmpty(searchText))
                {
                    var entityType = typeof(Vehicle).Name;
                    var searchTableRepository = IocManager.Resolve<ISearchTableRepository>();
                    var fullTextSearchText = searchText;// FtsInterceptor.Fts(searchText);

                    query = from search in searchTableRepository.All()
                            join Vehicle in query on search.EntityId equals Vehicle.Id.ToString()
                            where search.EntityType == entityType && search.SearchData.Contains(fullTextSearchText)
                            select Vehicle;
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

        public List<PastContactRecordReportModel> GetFilterContactRecord(int VehicleId)
        {
            using (IocManager.BeginScope())
            {
                var personRepository = IocManager.Resolve<IPersonRepository>();
                var contactRecordRepository = IocManager.Resolve<IContactRecordRepository>();
                var PickListlcvRepository = IocManager.Resolve<IPickListRepository>();
                var PickListPartRepository = IocManager.Resolve<IPickListRepository>();
                var EditionRepository = IocManager.Resolve<IEditionRepository>();
                var query = from contact in contactRecordRepository.All()
                            join person in personRepository.All() on contact.PressMemberId equals person.Id into pers
                            join PickListlcv in PickListlcvRepository.All() on contact.LcvId equals PickListlcv.Id into lcv
                            join pickListPart in PickListPartRepository.All() on contact.participationStatus equals pickListPart.Id into part
                            join editionn in EditionRepository.All() on contact.EditionId equals editionn.Id into editions
                            from editionss in editions.DefaultIfEmpty()
                            from perss in pers.DefaultIfEmpty()
                            from lcvs in lcv.DefaultIfEmpty()
                            from parts in part.DefaultIfEmpty()

                            where contact.ContactKindId == 23 && contact.ContactTypeId == VehicleId
                            orderby contact.ContactDate
                            select new PastContactRecordReportModel
                            {
                                firstNamelastName = perss.FirstName == null || perss.LastName == null ? "-" : perss.FirstName + " " + perss.LastName,
                                Note = contact.Notes==null?"-":contact.Notes,
                                lcvName = lcvs.Name == null ? "-" : lcvs.Name,
                                particialName = parts.Name == null ? "-" : parts.Name,
                                date = contact.ContactDate,
                                EditionId = contact.EditionId,
                                EditionName=editionss.Name==null?"-":editionss.Name,
                                ContactKindId = contact.ContactKindId,
                                ContactTypeId = contact.ContactTypeId,
                                Id = contact.Id
                            };

                return query.OrderByDescending(x=>x.date).ToList();
            }
        }
    }
}
