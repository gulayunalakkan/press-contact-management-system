using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Manager;
using BasinTakip.Domain.Repository;
using BasinTakip.EntityFramework.Repository;
using BasinTakip.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Web.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult EventReport()
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
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
                return View(model);
            }
            //var model = new PastContactRecordReportModel();
            //model.UpComingEVentList = IocManager.Resolve<IReportManager>().Backreport();
            //ViewBag.BackClass = "viewbag_Back";
            //return View(model);
        }

        public ActionResult mounthlybirthday()
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            using (IocManager.BeginScope())
            {
                var model = new PastContactRecordReportModel();
                var personRepository = IocManager.Resolve<IPersonRepository>();
                var contactRepository = IocManager.Resolve<IContactRecordRepository>();

                var queryBirthDateList = (
                    from person in personRepository.All()
                    where person.BirthDate.Value.Month == DateTime.Now.Month && person.IsDeleted == false
                    orderby person.BirthDate descending
                    select new PastContactRecordReportModel
                    {
                        Id = person.Id,
                        firstNamelastName = person.FirstName + " " + person.LastName,
                        EditionName= person.EditionNames,
                        PressMemberBirthDate = person.BirthDate
                    }
                    ).OrderBy(x => x.PressMemberBirthDate.Value.Day);
                model.MountBirthDate = queryBirthDateList.ToList();
                return View(model);
            }
            //var model = new PastContactRecordReportModel();
            //model.UpComingEVentList = IocManager.Resolve<IReportManager>().Backreport();
            //ViewBag.BackClass = "viewbag_Back";
            //return View(model);
        }

        public ActionResult LastContactReport()
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            using (IocManager.BeginScope())
            {
                var model = new PastContactRecordReportModel();
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
                                        join press in personRepository.All() on contact.PressMemberId equals press.Id into person
                                        join editon in editionRepository.All() on contact.EditionId equals editon.Id into edition
                                        from editions in edition.DefaultIfEmpty()
                                        from persons in person.DefaultIfEmpty()
                                        from vehicles in vehicle.DefaultIfEmpty()
                                        from eventtypes in eventtype.DefaultIfEmpty()
                                        where contact.ContactDate < DateTime.Now && contact.IsDeleted == false
                                        orderby contact.ContactDate descending

                                        select new PastContactRecordReportModel
                                        {
                                            firstNamelastName = persons.FirstName + " " + persons.LastName,
                                            Id = contact.Id,
                                            EventName = events.Name == null ? "-" : events.Name,
                                            EditionName = editions.Name == null ? "-" : editions.Name,
                                            LastContactDate = contact.ContactDate
                                            // EventPlacename = events.EventPlace==null?"-":events.EventPlace,
                                            // EventTypeName = eventtypes.Name==null?" ":eventtypes.Name,
                                            // LastContactDate = contact.ContactDate,

                                        });
                model.HappeningLastContactList = queryLastContact.ToList();
                return View(model);
            }
            //var model = new PastContactRecordReportModel();
            //model.HappeningLastContactList = IocManager.Resolve<IReportManager>().LastContactReport();
            //return View(model);
        }

        public ActionResult ContactReport()
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            using (IocManager.BeginScope())
            {
                var model = new PastContactRecordReportModel();

                var personRepository = IocManager.Resolve<IPersonRepository>();
                var contactRepository = IocManager.Resolve<IContactRecordRepository>();
                var eventRepository = IocManager.Resolve<IEventRepository>();
                var taskRepository = IocManager.Resolve<IPickListRepository>();
                var editionRepository = IocManager.Resolve<IEditionRepository>();
                var vehicleRepository = IocManager.Resolve<IVehicleRepository>();
                var pickListKindRepository = IocManager.Resolve<IPickListRepository>();
                var pickListContacttypeRepository = IocManager.Resolve<IPickListRepository>();

                //DateTime BackDate = DateTime.Now.AddMonths(-6);
                
               // var queryNoContact = (from person in personRepository.All()
               //                       join contact in contactRepository.All() on person.Id equals contact.PressMemberId
               //                       join task in taskRepository.All() on person.TaskId equals task.Id into task
               //                       join edition in editionRepository.All() on person.EditionId equals edition.Id into edition
               //                       join eventt in eventRepository.All() on contact.ContactTypeSubId equals eventt.Id into evnt
               //                       join vehicle in vehicleRepository.All() on contact.ContactTypeId equals vehicle.Id into vhc
               //                       join kind in pickListKindRepository.All() on contact.ContactKindId equals kind.Id into kind
               //                       join contactType in pickListContacttypeRepository.All() on contact.ContactTypeId equals contactType.Id into ctype
               //                       from ctypes in ctype.DefaultIfEmpty()
               //                       from vhcs in vhc.DefaultIfEmpty()
               //                       from evnts in evnt.DefaultIfEmpty()
               //                       from editions in edition.DefaultIfEmpty()
               //                       from tasks in task.DefaultIfEmpty()
               //                       from kinds in kind.DefaultIfEmpty()
               //                       where contact.ContactDate < BackDate && contact.IsDeleted == false
               //                       select new PastContactRecordReportModel
               //                       {
               //                           Id = person.Id,
               //                           firstNamelastName = person.FirstName + " " + person.LastName,
               //                           MobilePhone = person.MobilePhone == null ? "-" : person.MobilePhone,
               //                           Email = person.Email == null ? "-" : person.Email,
               //                           TaskName = tasks.Name == null ? "-" : tasks.Name,
               //                           EditionName = editions.Name == null ? "-" : editions.Name,
               //                           LastContactDate = contact.ContactDate == null ? DateTime.MinValue : contact.ContactDate,
               //                           ContactTypeName = contact.ContactKindId == 22 ? ctypes.Name : kinds.Name,
               //                           ContactSubName = contact.ContactKindId == 22 ? evnts.Name : vhcs.Serial
               //                       });
               // //   queryNoContact.GroupBy(x=>x.Email).SelectMany(g=>g.Select((j)=> new { j.Email,date=j)})).OrderBy()
               //var test= queryNoContact.GroupBy(student => student.Email)
               //        .Select(group =>
               //        new
               //        {
               //            Name = group.Key,
               //            students = group.OrderByDescending(x => x.LastContactDate)
               //        })
               //        .OrderBy(group => group.students.First().LastContactDate);
                //var tests = test.ToList();
                //model.NoContactPressMemberList = queryNoContact.ToList();
                model.NoContactPressMemberList= eventRepository.PastContactRecord(int.MaxValue);
                return View(model);
            }
            //using (IocManager.BeginScope())
            //{
            //    var model = new PastContactRecordReportModel();
            //    model.NoContactPressMemberList = IocManager.Resolve<IReportManager>().ContactReport();
            //    ViewBag.BackClass = "viewbag_Back";
            //    return View(model);
            //}

        }
    }
}