using BasinTakip.Domain.Repository;
using BasinTakip.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Application
{
    public class ReportManager
    {
        public List<PastContactRecordReportModel> Backreport()
        {
            using (IocManager.BeginScope())
            {
                var model = new PastContactRecordReportModel();
                var personRepository = IocManager.Resolve<IPersonRepository>();
                var contactRepository = IocManager.Resolve<IContactRecordRepository>();
                var eventRepository = IocManager.Resolve<IEventRepository>();
                var taskRepository = IocManager.Resolve<IPickListRepository>();
                var editionRepository = IocManager.Resolve<IEditionRepository>();
                var vehicleRepository = IocManager.Resolve<IVehicleRepository>();

                var queryEvent = (from events in eventRepository.All()
                                  join eventType in taskRepository.All() on events.EventTypeId equals eventType.Id into evntype
                                  from eventtypes in evntype.DefaultIfEmpty()
                                  where events.BeginDate > DateTime.Now && events.IsDeleted == false
                                  orderby events.BeginDate ascending
                                  select new PastContactRecordReportModel
                                  {
                                      Id = events.Id,
                                      EventName = events.Name==null?"-":events.Name,
                                      EventPlacename = events.EventPlace==null?"-":events.EventPlace,
                                      EventTypeName = eventtypes.Name==null?"-":eventtypes.Name,
                                      EventDate = events.BeginDate
                                  });
                model.UpComingEVentList = queryEvent.ToList();
                return queryEvent.ToList();
            }
        }

        public List<PastContactRecordReportModel> LastContactReport()
        {
            using (IocManager.BeginScope())
            {
                var personRepository = IocManager.Resolve<IPersonRepository>();
                var contactRepository = IocManager.Resolve<IContactRecordRepository>();
                var eventRepository = IocManager.Resolve<IEventRepository>();
                var taskRepository = IocManager.Resolve<IPickListRepository>();
                var editionRepository = IocManager.Resolve<IEditionRepository>();
                var vehicleRepository = IocManager.Resolve<IVehicleRepository>();
                var queryLastContact = (from contact in contactRepository.All()
                                        join events in eventRepository.All() on contact.ContactTypeSubId equals events.Id
                                        join eventtype in taskRepository.All() on events.EventTypeId equals eventtype.Id into eventtype
                                        join vehicle in vehicleRepository.All() on contact.ContactTypeId equals vehicle.Id into vehicle
                                        from vehicles in vehicle.DefaultIfEmpty()
                                        from eventtypes in eventtype.DefaultIfEmpty()
                                        where contact.ContactDate < DateTime.Now && contact.IsDeleted == false
                                        orderby contact.ContactDate descending
                                        select new PastContactRecordReportModel
                                        {
                                            Id = contact.Id,
                                            EventName = events.Name==null?"-":events.Name,
                                            EventPlacename = events.EventPlace==null?"-":events.EventPlace,
                                            EventTypeName = eventtypes.Name==null?"-":eventtypes.Name,
                                            LastContactDate = contact.ContactDate,

                                        });
                return queryLastContact.ToList();
            }
        }

        public List<PastContactRecordReportModel> ContactReport()
        {
            using (IocManager.BeginScope())
            {
                var personRepository = IocManager.Resolve<IPersonRepository>();
                var contactRepository = IocManager.Resolve<IContactRecordRepository>();
                var eventRepository = IocManager.Resolve<IEventRepository>();
                var taskRepository = IocManager.Resolve<IPickListRepository>();
                var editionRepository = IocManager.Resolve<IEditionRepository>();
                var vehicleRepository = IocManager.Resolve<IVehicleRepository>();
                var pickListKindRepository = IocManager.Resolve<IPickListRepository>();
                var pickListContacttypeRepository = IocManager.Resolve<IPickListRepository>();

                DateTime BackDate = DateTime.Now.AddMonths(-6);
                var queryNoContact = (from person in personRepository.All()
                                      join contact in contactRepository.All() on person.Id equals contact.PressMemberId
                                      join task in taskRepository.All() on person.TaskId equals task.Id into task
                                      join edition in editionRepository.All() on person.EditionId equals edition.Id into edition
                                      join eventt in eventRepository.All() on contact.ContactTypeSubId equals eventt.Id into evnt
                                      join vehicle in vehicleRepository.All() on contact.ContactTypeId equals vehicle.Id into vhc
                                      join kind in pickListKindRepository.All() on contact.ContactKindId equals kind.Id into kind
                                      join contactType in pickListContacttypeRepository.All() on contact.ContactTypeId equals contactType.Id into ctype
                                      from ctypes in ctype.DefaultIfEmpty()
                                      from vhcs in vhc.DefaultIfEmpty()
                                      from evnts in evnt.DefaultIfEmpty()
                                      from editions in edition.DefaultIfEmpty()
                                      from tasks in task.DefaultIfEmpty()
                                      from kinds in kind.DefaultIfEmpty()
                                      where contact.ContactDate < BackDate && contact.IsDeleted == false
                                      select new PastContactRecordReportModel
                                      {
                                          Id = person.Id,
                                          firstNamelastName = person.FirstName == null || person.LastName == null ? "-" : person.FirstName + " " + person.LastName,
                                          MobilePhone = person.MobilePhone==null?"-":person.MobilePhone,
                                          Email = person.Email==null?"-":person.Email,
                                          TaskName = tasks.Name == null ? "-" : tasks.Name,
                                          EditionName = editions.Name == null ? "-" : editions.Name,
                                          LastContactDate = contact.ContactDate,
                                          ContactTypeName = contact.ContactKindId == 22 ? ctypes.Name : kinds.Name,
                                          ContactSubName = contact.ContactKindId == 22 ? evnts.Name : vhcs.Serial
                                      });
                return queryNoContact.ToList();
            }
        }
    }
}