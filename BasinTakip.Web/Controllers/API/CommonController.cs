using BasinTakip.Core.Web;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Manager;
using BasinTakip.Domain.Repository;
using BasinTakip.EntityFramework.Repository;
using BasinTakip.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BasinTakip.Web.Controllers.API
{
    public class CommonController : ApiController
    {
        [System.Web.Http.HttpPost]
        public List<Event> PickList(CommonModel input)
        {
            var result = new List<Event>();
            var eventManager = IocManager.Resolve<IEventManager>();
            result = eventManager.Filter(x => x.EventTypeId == input.Id  && x.IsDeleted==false).ToList();
            return result;
        }

        [System.Web.Http.HttpPost]
        public List<PressPickListModel> pressMember(CommonModel input)
        {
            List<PressPickListModel> pressMemberlist = new List<PressPickListModel>();
            //var result = new List<PressMember>();

            using (IocManager.BeginScope())
            {
                var editionRepository = IocManager.Resolve<IEditionRepository>();
              var test = editionRepository.PastContactListEditionWithPress(input.Id);
         
            var pressmemberManager = IocManager.Resolve<IPersonManager>();
           // result = pressmemberManager.Filter(x => x.EditionIds.Contains(input.Id.ToString()) && x.IsDeleted==false).ToList();
            foreach (var item in test )
            {
                PressPickListModel newPressMember = new PressPickListModel();
                    newPressMember.Name = item.firstNamelastName;
                newPressMember.Id = item.Id;
                pressMemberlist.Add(newPressMember);
            }
            }

            return pressMemberlist;
        }

        [System.Web.Http.HttpPost]
        public List<PickList> getPickListEvent()
        {
            var picklistEvent = IocManager.Resolve<IPickListManager>();
            return picklistEvent.Filter(x => x.CategoryId == 2 && x.IsDeleted==false).OrderBy(X=>X.Name).ToList();
        }

        [System.Web.Http.HttpPost]
        public List<Vehicle> getVehicle()
        {
            var vehicleRepository = IocManager.Resolve<IVehicleManager>();
            return vehicleRepository.Filter(x => x.IsDeleted == false).OrderBy(X=>X.Model).ToList();
        }

        [System.Web.Http.HttpPost]
        public List<District> getDistrictList(CommonModel input)
        {
            var districtRepository = new DistrictRepository();
            return districtRepository.Filter(x=>x.CityId==input.Id).OrderBy(x=>x.Name).ToList();
        }

        [System.Web.Http.HttpPost]
        public List<EventSpecial> getCalendarList()
        {
            var eventcalendarRepository = new EventCalendarRepository();
            return eventcalendarRepository.GetEventCalendarList();
            //var eventmanager = IocManager.Resolve<IEventManager>();
            //return eventmanager.All().ToList();
        }

        [System.Web.Http.HttpPost]
        public bool  EmailDuplicate(CommonModel Email)
        {
            List<PressPickListModel> pressMemberlist = new List<PressPickListModel>();
            var result = new List<PressMember>();
            var pressmemberManager = IocManager.Resolve<IPersonManager>();
            result = pressmemberManager.Filter(x => x.Email == Email.Email && x.IsDeleted == false).ToList();
            if(result.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }
    }
}
