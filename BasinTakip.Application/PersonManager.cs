using BasinTakip.Domain.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Repository;
using BasinTakip.Core;
using BasinTakip.Core.Business;
using BasinTakip.Domain.Entities.Abstract;
using PagedList;
using BasinTakip.Core.Data;
using BasinTakip.Web.Models;
using BasinTakip.EntityFramework.Repository;

namespace BasinTakip.Application
{
    public class PersonManager : GenericManager<IPersonRepository, PressMember, int>, IPersonManager
    {
        public List<PastContactRecordReportModel> GetFilterContactRecord(int pressMemberId)
        {
            using (IocManager.BeginScope())
            {
                var personRepository = IocManager.Resolve<IPersonRepository>();
                var contactRecordRepository = IocManager.Resolve<IContactRecordRepository>();
                var PickListlcvRepository = IocManager.Resolve<IPickListRepository>();
                var PickListPartRepository = IocManager.Resolve<IPickListRepository>();
                var PickListTaskRepository = IocManager.Resolve<IPickListRepository>();
                var eventRepository = IocManager.Resolve<IEventRepository>();
                var vehicleRepository = IocManager.Resolve<IVehicleRepository>();
                var pickListKindRepository = IocManager.Resolve<IPickListRepository>();
                var pickListContacttypeRepository = IocManager.Resolve<IPickListRepository>();
                var EditionRepository = IocManager.Resolve<IEditionRepository>();
                var query = from contact in contactRecordRepository.All()
                            join person in personRepository.All() on contact.PressMemberId equals person.Id
                            join PickListlcv in PickListlcvRepository.All() on contact.LcvId equals PickListlcv.Id into lcv
                            join pickListPart in PickListPartRepository.All() on contact.participationStatus equals pickListPart.Id into part
                            join events in eventRepository.All() on contact.ContactTypeSubId equals events.Id into evnt
                            join vehicles in vehicleRepository.All() on contact.ContactTypeId equals vehicles.Id into vehcl
                            join kind in pickListKindRepository.All() on contact.ContactKindId equals kind.Id into kind
                            join contactType in pickListContacttypeRepository.All() on contact.ContactTypeId equals contactType.Id into ctype
                            join edition in EditionRepository.All() on contact.EditionId equals edition.Id into editions
                            
                            from editionss in editions.DefaultIfEmpty()
                            from vehicles in vehcl.DefaultIfEmpty()
                            from evnts in evnt.DefaultIfEmpty()
                            from lcvs in lcv.DefaultIfEmpty()
                            from parts in part.DefaultIfEmpty()
                            from ctypes in ctype.DefaultIfEmpty()
                            from kinds in kind.DefaultIfEmpty()
                            orderby contact.Id
                            where contact.PressMemberId == pressMemberId
                            select new PastContactRecordReportModel
                            {
                                Id = contact.Id,
                                firstNamelastName =person.FirstName+" "+person.LastName,
                                Note = contact.Notes==null?"-":contact.Notes,
                                lcvName = lcvs.Name == null ? " - " : lcvs.Name,
                                particialName = parts.Name == null ? " - " : parts.Name,
                                date = contact.ContactDate,
                                LastContactDate=contact.ContactEndDate,
                                ContactBackName = kinds.Id == 22 ? ctypes.Name == null ? " - " : ctypes.Name + " - " + evnts.Name : kinds.Name + " - " + vehicles.Serial,
                                EditionId = contact.EditionId,
                                ContactKindId = contact.ContactKindId,
                                ContactTypeId = contact.ContactTypeId,
                                ContactSubId = kinds.Id == 22 ? contact.ContactTypeSubId : null,
                                EditionName=editionss.Name,
                            };
                var queryNew = from cnt in query.ToList()
                           select new PastContactRecordReportModel
                           {
                               ContactBackName = cnt.ContactBackName,
                               Id = cnt.Id,
                               firstNamelastName = cnt.firstNamelastName,
                               Note = cnt.Note,
                               lcvName = cnt.lcvName,
                               particialName = cnt.particialName,
                               date = cnt.date,
                               EditionId = cnt.EditionId,
                               ContactKindId = cnt.ContactKindId,
                               ContactTypeId = cnt.ContactTypeId,
                               ContactSubId = cnt.ContactSubId,
                               EditionName=cnt.EditionName,
                               LastContactDate = cnt.LastContactDate,
                           };
                return queryNew.OrderByDescending(x=>x.date).ToList();
            }
        }

        public IPagedList<PersonResult> GetInclueded(int pageNumber, int pageSize, DateTime? contactDate,
            int? editionId, int? taskId, int? cityId,int? districtId=null, bool? block=null, string orderByColumn = "Id", bool orderType = false, string searchText = null,int? BeginYear=null,int? BeginMonth=null)
        {
            using (IocManager.BeginScope())
            {
                var personRepository = IocManager.Resolve<IPersonRepository>();
                var editionRepository = IocManager.Resolve<IEditionRepository>();
                var PickListRepository = IocManager.Resolve<IPickListRepository>();
                var contactRepository = IocManager.Resolve<IContactRecordRepository>();

                var result = personRepository.All();
               
                
                    if (editionId.HasValue)
                {
                    var EditionName = editionRepository.Filter(x => x.Id == editionId).SingleOrDefault();
                    // result = personRepository.Filter(x => x.EditionIds.Contains(editionId.ToString()));
                    result = personRepository.Filter(x => x.EditionNames.Contains(EditionName.Name));
                }
                if (cityId.HasValue)
                {
                    result = result.Where(x => x.CountryId == cityId);
                }
                if (districtId.HasValue)
                {
                    result = result.Where(x => x.DistrictId == districtId);
                }
                if (taskId.HasValue)
                {
                    result = result.Where(x => x.TaskId == taskId);
                }
                if (block==true)
                {
                    result = result.Where(x => x.Block == true);
                }
              
               
                if (BeginYear != null && BeginMonth!=null)
                {
                    var innerQuery = from person in result
                                     join contact in contactRepository.All() on person.Id equals contact.PressMemberId into cnt
                                     join PickList in PickListRepository.All() on person.TaskId equals PickList.Id into pcl
                                     from pcls in pcl.DefaultIfEmpty()
                                     from cntt in cnt.DefaultIfEmpty()
                                     where cntt.ContactDate.Year == BeginYear
                                     orderby person.Id
                                     select new PersonResult
                                     {
                                         Adsoyad =person.FirstName==null||person.LastName==null?"-": person.FirstName + " " + person.LastName,
                                         Id = person.Id,
                                         EditionName =person.EditionNames==null?"-":person.EditionNames,
                                         IsActive = person.IsActive,
                                         FirstName = person.FirstName==null?"-":person.FirstName,
                                         FirmName = person.EditionNames == null?"-": person.EditionNames,
                                         LastName = person.LastName==null?"-":person.LastName,
                                         MobilePhone = person.MobilePhone == null ? " - " : person.MobilePhone,
                                         Email = person.Email==null?"-":person.Email,
                                         Email2 = person.Email2==null?"-":person.Email2,
                                         TaskName = pcls.Name==null?" ":pcls.Name,
                                         CreatedAt = person.CreatedAt,
                                         AdSoyadContains=person.FirstName+person.LastName
                                         
                                     };

                    if (!string.IsNullOrEmpty(searchText))
                    {
                        
                        innerQuery = innerQuery.Where(x => x.AdSoyadContains.Replace(" ","").Contains(searchText.Replace(" ","")) || x.EditionName.Contains(searchText));

                    }
                    if (orderType)
                    {
                        innerQuery = innerQuery.OrderByDescending(orderByColumn);
                    }
                    else
                    {
                        innerQuery = innerQuery.OrderBy(orderByColumn);
                    }

                    return innerQuery.ToPagedList(pageNumber, pageSize);
                }
                if (BeginMonth != null && BeginYear==null)
                {
                    var innerQuery = from person in result
                                     join contact in contactRepository.All() on person.Id equals contact.PressMemberId into cnt
                                     join PickList in PickListRepository.All() on person.TaskId equals PickList.Id into pcl
                                     from pcls in pcl.DefaultIfEmpty()
                                     from cntt in cnt.DefaultIfEmpty()
                                     where cntt.ContactDate.Month == BeginMonth
                                     orderby person.Id
                                     select new PersonResult
                                     {
                                         Adsoyad = person.FirstName == null || person.LastName == null ? "-" : person.FirstName + " " + person.LastName,
                                         Id = person.Id,
                                         EditionName = person.EditionNames == null ? "-" : person.EditionNames,
                                         IsActive = person.IsActive,
                                         FirstName = person.FirstName == null ? "-" : person.FirstName,
                                         FirmName = person.EditionNames == null ? "-" : person.EditionNames,
                                         LastName = person.LastName == null ? "-" : person.LastName,
                                         MobilePhone = person.MobilePhone == null ? " - " : person.MobilePhone,
                                         Email = person.Email == null ? "-" : person.Email,
                                         Email2 = person.Email2 == null ? "-" : person.Email2,
                                         TaskName = pcls.Name == null ? " " : pcls.Name,
                                         CreatedAt = person.CreatedAt,
                                         AdSoyadContains = person.FirstName + person.LastName
                                     };

                    if (!string.IsNullOrEmpty(searchText))
                    {
                        innerQuery = innerQuery.Where(x => x.AdSoyadContains.Replace(" ", "").Contains(searchText.Replace(" ", "")) || x.EditionName.Contains(searchText));

                    }
                    if (orderType)
                    {
                        innerQuery = innerQuery.OrderByDescending(orderByColumn);
                    }
                    else
                    {
                        innerQuery = innerQuery.OrderBy(orderByColumn);
                    }

                    return innerQuery.ToPagedList(pageNumber, pageSize);
                }
                if (BeginMonth == null && BeginYear != null)
                {
                    var innerQuery = from person in result
                                     join contact in contactRepository.All() on person.Id equals contact.PressMemberId into cnt
                                     join PickList in PickListRepository.All() on person.TaskId equals PickList.Id into pcl
                                     from pcls in pcl.DefaultIfEmpty()
                                     from cntt in cnt.DefaultIfEmpty()
                                     where cntt.ContactDate.Month == BeginMonth & cntt.ContactDate.Year==BeginMonth
                                     orderby person.Id
                                     select new PersonResult
                                     {
                                         Adsoyad = person.FirstName == null || person.LastName == null ? "-" : person.FirstName + " " + person.LastName,
                                         Id = person.Id,
                                         EditionName = person.EditionNames == null ? "-" : person.EditionNames,
                                         IsActive = person.IsActive,
                                         FirstName = person.FirstName == null ? "-" : person.FirstName,
                                         FirmName = person.EditionNames == null ? "-" : person.EditionNames,
                                         LastName = person.LastName == null ? "-" : person.LastName,
                                         MobilePhone = person.MobilePhone == null ? " - " : person.MobilePhone,
                                         Email = person.Email == null ? "-" : person.Email,
                                         Email2 = person.Email2 == null ? "-" : person.Email2,
                                         TaskName = pcls.Name == null ? " " : pcls.Name,
                                         CreatedAt = person.CreatedAt,
                                         AdSoyadContains = person.FirstName + person.LastName
                                     };

                    //if (!string.IsNullOrEmpty(searchText))
                    //{
                    //    var entityType = typeof(PressMember).Name;
                    //    var searchTableRepository = IocManager.Resolve<ISearchTableRepository>();
                    //    var fullTextSearchText = searchText;// FtsInterceptor.Fts(searchText);

                    //    innerQuery = from search in searchTableRepository.All()
                    //            join PressMember in innerQuery on search.EntityId equals PressMember.Id.ToString()
                    //            where search.EntityType == entityType && search.SearchData.Contains(fullTextSearchText)
                    //            select PressMember;
                    //}
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        innerQuery = innerQuery.Where(x => x.AdSoyadContains.Replace(" ", "").Contains(searchText.Replace(" ", "")) || x.EditionName.Contains(searchText));

                    }
                    if (orderType)
                    {
                        innerQuery = innerQuery.OrderByDescending(orderByColumn);
                    }
                    else
                    {
                        innerQuery = innerQuery.OrderBy(orderByColumn);
                    }

                    return innerQuery.ToPagedList(pageNumber, pageSize);
                }

                var query = from person in result
                            join PickList in PickListRepository.All() on person.TaskId equals PickList.Id into pcl
                            from pcls in pcl.DefaultIfEmpty()
                            orderby person.Id
                            select new PersonResult
                            {
                                Adsoyad = person.FirstName == null || person.LastName == null ? "-" : person.FirstName + " " + person.LastName,
                                Id = person.Id,
                                EditionName = person.EditionNames == null ? "-" : person.EditionNames,
                                IsActive = person.IsActive,
                                FirstName = person.FirstName==null?"-":person.FirstName,
                                FirmName = person.EditionNames==null?"-": person.EditionNames,
                                LastName = person.LastName==null?"-":person.LastName,
                                MobilePhone = person.MobilePhone == null ? " - " : person.MobilePhone,
                                Email = person.Email==null?"-":person.Email,
                                Email2 = person.Email2==null?"-":person.Email2,
                                TaskName = pcls.Name==null?" ":pcls.Name,
                                CreatedAt = person.CreatedAt,
                                AdSoyadContains = person.FirstName + person.LastName
                            };

                if (!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(x => x.AdSoyadContains.Replace(" ", "").Contains(searchText.Replace(" ", "")) || x.EditionName.Contains(searchText));
                    //var entityType = typeof(PressMember).Name;
                    //var searchTableRepository = IocManager.Resolve<ISearchTableRepository>();
                    //var fullTextSearchText = searchText;// FtsInterceptor.Fts(searchText);

                    //query = from search in searchTableRepository.All()
                    //        join PressMember in query on search.EntityId equals PressMember.Id.ToString()
                    //        where search.EntityType == entityType && search.SearchData.Contains(fullTextSearchText)
                    //        select PressMember;

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
        public List<PastContactRecordReportModel> GetFilterPressMember(int pressMemberId)
        {
            using (IocManager.BeginScope())
            {
                var personRepository = IocManager.Resolve<IPersonRepository>();
                var PickListDistrictRepository = IocManager.Resolve<IPickListRepository>();
                var PickListCountryRepository = IocManager.Resolve<IPickListRepository>();
                var PickListTaskRepository = IocManager.Resolve<IPickListRepository>();
                var districtRepository = new DistrictRepository();
                var CityRepository = new CityRepository();

                var query = from person in personRepository.All()
                            join district in districtRepository.All() on person.DistrictId equals district.Id into dst
                            join country in CityRepository.All() on person.CountryId equals country.Id into cnt
                            join task in PickListTaskRepository.All() on person.TaskId equals task.Id into taskk

                            from tasks in taskk.DefaultIfEmpty()
                            from dstrct in dst.DefaultIfEmpty()
                            from cntry in cnt.DefaultIfEmpty()
                            select new PastContactRecordReportModel
                            {
                                Id = person.Id,
                                firstNamelastName = person.FirstName.Length + person.LastName.Length < 25 ? person.FirstName + " " + person.LastName : person.FirstName.Substring(0, 10) + "...",
                                Email = person.Email,
                                CountryName = cntry.Name,
                                Note = person.Notes == null ? "-" : person.Notes,
                                MobilePhone = person.MobilePhone,
                                EditionNames = person.EditionNames,
                                PressMemberBirthDate = person.BirthDate,
                                TaskName = tasks.Name == null ? "-" : tasks.Name,
                                About = person.About,
                                Email2 = person.Email2,
                            };
                return query.OrderBy(x => x.Ad).ToList();
            }
        }

    }
}
