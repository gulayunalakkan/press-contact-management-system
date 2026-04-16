using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Repository;
using BasinTakip.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BasinTakip.EntityFramework.Repository
{
    public class ContactRecordRepository : GenericRepository<CommonContext, ContactRecord, int>, IContactRecordRepository
    {
        public ContactRecordRepository(CommonContext context)
            : base(context)
        {
        }
        protected override Expression<Func<ContactRecord, bool>> FindyByKeyExpression(int key)
        {
            return p => p.Id == key;
        }

        public override string GetSearchData(ContactRecord entity)
        {
            string result = base.GetSearchData(entity);

            var pickListRepository = IocManager.Resolve<IPickListRepository>();
            var pressMemberRepository = IocManager.Resolve<IPersonRepository>();
            var editionRepository = IocManager.Resolve<IEditionRepository>();
            var eventRepository = IocManager.Resolve<IEventRepository>();
            var vehicleRepository = IocManager.Resolve<IVehicleRepository>();
            var contactTypeKind = entity.ContactKindId == 22 ? "Etkinlik" : "Araç Tahsisi";
            var contactTypeName = ""; var contactTypeSubName = "";
            if (entity.ContactKindId == 22)
            {
                contactTypeName = string.Join(" ", from p in pickListRepository.All()
                                                   where p.CategoryId == 2 && p.Id == entity.ContactTypeId
                                                   select p.Name);
                contactTypeSubName = string.Join(" ", from events in eventRepository.All()
                                                      where events.Id == entity.ContactTypeSubId
                                                      select events.Name);

            }
            else
            {
                contactTypeName = string.Join(" ", from vehicle in vehicleRepository.All()
                                                   where vehicle.Id == entity.ContactTypeId
                                                   select vehicle.Marka + vehicle.Model);
            }

            var pressMember = string.Join("", from p in pressMemberRepository.All() where p.Id == entity.PressMemberId select p.FirstName +" "+ p.LastName);

            var lcv = string.Join(" ", from p in pickListRepository.All()
                                       where p.CategoryId == 7 && p.Id == entity.LcvId
                                       select p.Name);

            var partic = string.Join(" ", from p in pickListRepository.All()
                                          where p.CategoryId == 6 && p.Id == entity.participationStatus
                                          select p.Name);
            var editonName = string.Join(" ", from edition in editionRepository.All()
                                              where edition.Id == entity.EditionId
                                              select edition.Name);
            result += " " + (contactTypeName ?? string.Empty) + " " + (pressMember ?? string.Empty) + " " + (editonName ?? string.Empty) + " " + (contactTypeKind ?? string.Empty) + " " + (contactTypeName ?? string.Empty) + " " + (contactTypeSubName ?? string.Empty) + " " + (lcv ?? string.Empty) + " " + (partic ?? string.Empty);

            return result;
        }
    }
}
