using AutoMapper;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasinTakip.Web.App_Start
{
    public static class MapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<PressMember, PressMemberDetailModel>();
                cfg.CreateMap<PickList, PickListDetailModel>();
                cfg.CreateMap<Edition, EditionDetailModel>();
                cfg.CreateMap<Event, EventDetailModel>();
                cfg.CreateMap<ContactRecord, ContactRecordDetailModel>();
                cfg.CreateMap<ContactRecord, ContactRecordListModel>();
                cfg.CreateMap<Vehicle, VehicleDetailModel>();
            });
        }
    }
}