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
using BasinTakip.Web.Models;
using BasinTakip.EntityFramework.Repository;
using BasinTakip.Core.Data;

namespace BasinTakip.Application
{
    public class EventManager : GenericManager<IEventRepository, Event, int>, IEventManager
    {
        public IPagedList<EventResult> GetInclueded(int pageNumber, int pageSize, DateTime? begindate = null, string eventPlace = null, int? eventType = null, string orderByColumn = "Id", bool orderType = false, string searchText = null,int? BeginYear=null,int? beginmounth=null)
        {
            using (IocManager.BeginScope())
            {
                var eventRepository = IocManager.Resolve<IEventRepository>();
                var pickListRepository = IocManager.Resolve<IPickListRepository>();

                var events = eventRepository.Filter(p => p.IsDeleted == false);

                var contactRepository = IocManager.Resolve<IContactRecordRepository>();
                var pressRepository = IocManager.Resolve<IPersonRepository>();
                var testquery = eventRepository.GetContactPressMemberReport(pageNumber, pageSize, begindate, eventPlace, eventType,orderByColumn,orderType,searchText,BeginYear,beginmounth);
                foreach (var item in testquery)
                {
                   if( item.EventTypeName==null || item.EventTypeName=="")
                    {
                        item.EventTypeName = " ";
                    }
                }
                return testquery;
            }
        }
        public List<PastContactRecordReportModel> GetFilterContactRecord(int EventId) 
        {
            using (IocManager.BeginScope())
            {
                var personRepository = IocManager.Resolve<IPersonRepository>();
                var contactRecordRepository = IocManager.Resolve<IContactRecordRepository>();
                var PickListlcvRepository = IocManager.Resolve<IPickListRepository>();
                var PickListPartRepository = IocManager.Resolve<IPickListRepository>();
                var EditionRepository = IocManager.Resolve<IEditionRepository>();
                var EventRepository = IocManager.Resolve<IEventRepository>();
                var query = from contact in contactRecordRepository.All()
                            join person in personRepository.All() on contact.PressMemberId equals person.Id into pers
                            join PickListlcv in PickListlcvRepository.All() on contact.LcvId equals PickListlcv.Id into lcv
                            join pickListPart in PickListPartRepository.All() on contact.participationStatus equals pickListPart.Id into part
                            join Editions in EditionRepository.All() on contact.EditionId equals Editions.Id  into editsn
                            join events in EventRepository.All() on contact.ContactTypeSubId equals events.Id
                            from editionss in editsn.DefaultIfEmpty()
                            from perss in pers.DefaultIfEmpty()
                            from lcvs in lcv.DefaultIfEmpty()
                            from parts in part.DefaultIfEmpty()

                            where contact.ContactKindId == 22 && contact.ContactTypeSubId == EventId
                            orderby contact.Id
                            select new PastContactRecordReportModel
                            {
                                firstNamelastName =perss.FirstName==null||perss.LastName==null?"-": perss.FirstName + " " + perss.LastName,
                                Note = contact.Notes==null ?"-":contact.Notes,
                                lcvName = lcvs.Name == null ? "-" : lcvs.Name,
                                particialName = parts.Name == null ? "-" : parts.Name,
                                date = contact.ContactDate,
                                EditionId = contact.EditionId,
                                ContactKindId = contact.ContactKindId,
                                ContactTypeId = contact.ContactTypeId,
                                ContactSubId = contact.ContactTypeSubId,
                                Id = contact.Id,
                                EditionName = editionss.Name== null?"-": editionss.Name,
                                EventName =events.Name
                            };
             
                return query.OrderByDescending(x=>x.date).ToList();
            }
        }

    }
}
