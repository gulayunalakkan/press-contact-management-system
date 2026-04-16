using BasinTakip.Core.Business;
using BasinTakip.Domain.Entities.Abstract;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Manager;
using BasinTakip.Domain.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasinTakip.Core.Data;

namespace BasinTakip.Application
{
    public class ContactRecordManager : GenericManager<IContactRecordRepository, ContactRecord, int>, IContactRecordManager
    {
        public IPagedList<ContactRecordResult> GetInclueded(int pageNumber, int pageSize, DateTime? beginDate,
            int? editionId, int? contactTpypeId, string orderByColumn = "Id", bool orderType = false, string searchText = null,int? beginyear=null,int? beginmounth=null)
        {
            using (IocManager.BeginScope())
            {
                var contactRecordRepository = IocManager.Resolve<IContactRecordRepository>();
                var pickListRepository = IocManager.Resolve<IPickListRepository>();
                var pressMemberRepository = IocManager.Resolve<IPersonRepository>();
                var editionRepository = IocManager.Resolve<IEditionRepository>();
                var contactsubRepository = IocManager.Resolve<IEventRepository>();
                var contactKindRepository = IocManager.Resolve<IPickListRepository>();
                var vehicleRepository = IocManager.Resolve<IVehicleRepository>();
                
                var contact = contactRecordRepository.Filter(x => x.IsDeleted == false);
                if (beginyear!=null)
                {
                    contact = contact.Where(x => x.ContactDate.Year == beginyear);
                }
                if (beginmounth!=null)
                {
                    contact = contact.Where(x => x.ContactDate.Month == beginmounth);
                }
                //if (beginDate.HasValue)
                //{
                //    if (beginDate!=DateTime.MinValue)
                //    {
                //    contact = contact.Where(x => x.ContactDate.Year == beginDate.Value.Year & x.ContactDate.Month == beginDate.Value.Month);


                //    }
                //}
                if (editionId.HasValue)
                {
                    contact = contact.Where(x => x.EditionId == editionId);
                }
                if (contactTpypeId.HasValue)
                {
                    contact = contact.Where(x => x.ContactKindId == contactTpypeId);
                }
               
                var query = from contactRecord in contact
                            join pickList in pickListRepository.All() on contactRecord.ContactTypeId equals pickList.Id into pick
                            join picklistlcv in pickListRepository.All() on contactRecord.LcvId equals picklistlcv.Id into lcv
                            join pressmember in pressMemberRepository.All() on contactRecord.PressMemberId equals pressmember.Id into prs
                            join edition in editionRepository.All() on contactRecord.EditionId equals edition.Id into edtn
                            join participationstatus in pickListRepository.All() on contactRecord.participationStatus equals participationstatus.Id into status
                            join contactTypeSub in contactsubRepository.All() on contactRecord.ContactTypeSubId equals contactTypeSub.Id into sub
                            join contactKind in contactKindRepository.All() on contactRecord.ContactKindId equals contactKind.Id into kind
                            join vehicle in vehicleRepository.All() on contactRecord.ContactTypeId equals vehicle.Id into vehic
                            from vehics in vehic.DefaultIfEmpty()
                            from prss in prs.DefaultIfEmpty()
                            from subs in sub.DefaultIfEmpty()
                            from kinds in kind.DefaultIfEmpty()
                            from lcvs in lcv.DefaultIfEmpty()
                            from editn in edtn.DefaultIfEmpty()
                            from stats in status.DefaultIfEmpty()
                            from picks in pick.DefaultIfEmpty()
                      
                            orderby contactRecord.Id
                            select new ContactRecordResult
                            {
                                Id = contactRecord.Id,
                                EditionTypeName = editn.Name==null?"-":editn.Name,
                                ContactTypeName=contactRecord.ContactKindId==23 ? kinds.Name==null?" ":kinds.Name :picks.Name==null?" ":picks.Name,
                                PressMemberName = prss.FirstName==null|| prss.LastName==null?"-": prss.FirstName + " " + prss.LastName,
                                Notes = contactRecord.Notes == null ? "-" : contactRecord.Notes,
                                LcvName = lcvs.Name == null ? "-" : lcvs.Name,
                                participationStatusName = stats.Name == null ? "-" : stats.Name,
                                CreatedAt = contactRecord.CreatedAt,
                                ContactTypeSubName=contactRecord.ContactKindId==23?vehics.Serial==null?" ":vehics.Serial :subs.Name==null?" ":subs.Name,
                                ContactDate = contactRecord.ContactDate,

                            };

                //if (!string.IsNullOrEmpty(searchText))
                //{
                //    var entityType = typeof(ContactRecord).Name;
                //    var searchTableRepository = IocManager.Resolve<ISearchTableRepository>();
                //    var fullTextSearchText = searchText;// FtsInterceptor.Fts(searchText);

                //    query = from search in searchTableRepository.All()
                //            join contactRecord in query on search.EntityId equals contactRecord.Id.ToString()
                //            where search.EntityType == entityType && search.SearchData.Contains(fullTextSearchText)
                //            select contactRecord;
                //}
                if (!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(x => x.EditionTypeName.Contains(searchText) || x.ContactTypeName.Contains(searchText) || x.PressMemberName.Contains(searchText) || x.Notes.Contains(searchText) || x.LcvName.Contains(searchText) || x.participationStatusName.Contains(searchText) || x.ContactDate.ToString().Contains(searchText)).OrderBy(x => x.ContactDate);

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
