using BasinTakip.Core.Web;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Manager;
using BasinTakip.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasinTakip.Core.Entities.Abstract;
using BasinTakip.Domain.Entities.Abstract;
using AutoMapper;
using BasinTakip.Web.Models;

namespace BasinTakip.Web.Controllers
{
    public class ContactRecordController : BaseController<IContactRecordManager, IContactRecordRepository, ContactRecord, int>
    {
        public ContactRecordController(IContactRecordManager manager)
            : base(manager)
        {
        }
        public override ActionResult List(GenericListInput<ContactRecord, int> input)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);

            ViewBag.BackClass = "viewbag_Back";
            ViewBag.button = "btn-add";
            ViewBag.btnNew = "/ContactRecord/Detail";
            ViewBag.QueryString = "&BeginYear=" + input.BeginYear + "&BeginMonth=" + input.BeginMonth + "&EditionId=" + input.Edition + "&ContactTypeId=" + input.ContactType + "&SearchText=" + input.SearchText + "&OrderByColumn=" + input.OrderByColumn + "&OrderType=" + input.OrderType;
            //if (input.BeginYear != null & input.BeginMonth != null)
            //{
            //    input.BeginDate = new DateTime((int)input.BeginYear, (int)input.BeginMonth, 1);
            //}
         
            var editionResository = IocManager.Resolve<IEditionManager>();
            ViewBag.EditionList = editionResository.Filter(p => p.IsDeleted == false).OrderBy(x=>x.Name);
            var pickListRepository = IocManager.Resolve<IPickListManager>();
            ViewBag.ContactTypeList = pickListRepository.Filter(x => x.CategoryId == 3 && x.IsDeleted == false).OrderBy(x=>x.Name);
            var result = myManager.GetInclueded(input.PageNumber, input.PageSize, input.BeginDate, input.Edition
                , input.ContactType, input.OrderByColumn ?? "Id", input.OrderType, input.SearchText,input.BeginYear,input.BeginMonth);
            string[] filter = new string[10];
            filter[0] = input.BeginYear.ToString();
            filter[1] = input.BeginMonth.ToString();
            filter[2] = input.CityId.ToString();
            filter[3] = input.DistrictId.ToString();
            filter[4] = input.Edition.ToString();
            filter[5] = input.TaskId.ToString();
            filter[6] = input.SearchText;
            filter[7] = input.ContactType.ToString();
            filter[8] = input.OrderByColumn;
            filter[9] = input.OrderType.ToString();
            var model = new GenericListOutput<ContactRecordResult, int>
            {
                SearchText = input.SearchText,
                PagedData = result,
                filter = filter
            };
            return View(model);
        }
        public override ActionResult Detail(DetailInput<int> input = null)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            ViewBag.BackClass = "vievbag_detail";
            ViewBag.Back = "/ContactRecord/List";
            var entity = myManager.GetByKey(input.Id);
            if (entity != null)
            {
                ViewBag.DateTime = entity.CreatedAt.ToString("dd/MM/yyyy");
                ViewBag.createDate = "Kayıt Tarihi:";
            }
            if (entity == null) entity = new ContactRecord();

            var editionManager = IocManager.Resolve<IEditionManager>();
            var editionList = editionManager.Filter(p => p.IsDeleted == false);
            var pressMemberManager = IocManager.Resolve<IPersonManager>();
            var pressMemberList = pressMemberManager.Filter(p => p.IsDeleted == false);
            var contactTypeManager = IocManager.Resolve<IPickListManager>();
            var contactTypeList = contactTypeManager.Filter(x => x.CategoryId == 2 && x.IsDeleted == false);
            var contactTypealtManager = IocManager.Resolve<IEventManager>();
            var contactTypesubList = contactTypealtManager.Filter(p => p.IsDeleted == false);
            var lcvManager = IocManager.Resolve<IPickListManager>();
            var lcvList = lcvManager.Filter(x => x.CategoryId == 7 && x.IsDeleted == false);
            var participationManager = IocManager.Resolve<IPickListManager>();
            var participationList = participationManager.Filter(x => x.CategoryId == 6 && x.IsDeleted == false);
            var contactKindManager = IocManager.Resolve<IPickListManager>();
            var contactKindlist = contactKindManager.Filter(x => x.CategoryId == 3 && x.IsDeleted == false);
            var vehicleManager = IocManager.Resolve<IVehicleManager>();
            var vehicleList = vehicleManager.Filter(p => p.IsDeleted == false);
            var model = Mapper.Map<ContactRecordDetailModel>(entity);

            model.EditionTypeList = editionList.OrderBy(x=>x.Name).Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == entity.EditionId
            }).ToList();

            model.PressMemberList = pressMemberList.OrderBy(x => x.FirstName).Select(p => new SelectListItem
            {
                Text = p.FirstName + " " + p.LastName,
                Value = p.Id.ToString(),
                Selected = p.Id == entity.PressMemberId
            }).ToList();


            if (model.ContactKindId == 23)
            {
                model.ContactTypeList = vehicleList.Select(p => new SelectListItem
                {
                    Text = p.Model + " - " + p.Serial,
                    Value = p.Id.ToString(),
                    Selected = p.Id == entity.ContactTypeId
                }).ToList();
            }
            else
            {
                model.ContactTypeList = contactTypeList.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString(),
                    Selected = p.Id == entity.ContactTypeId
                }).ToList();
            }

            model.ContactTypeSubCategoryList = contactTypesubList.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == entity.ContactTypeSubId,
                Disabled = p.Id != entity.ContactTypeSubId
            }).ToList();
            model.LCVList = lcvList.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == entity.LcvId
            }).ToList();
            model.participationStatusList = participationList.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == entity.participationStatus
            }).ToList();
            model.ContactKindList = contactKindlist.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == entity.ContactKindId
            }).ToList();

            if (model.ContactDate == DateTime.MinValue)
            {
                model.ContactDate = DateTime.Now;
            }

            return View(model);
        }

        public ActionResult Delete(int Id)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            myManager.DeleteByKey(Id);
            return RedirectToAction("List");
        }

        public override ActionResult Detail(ContactRecord entity)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            if (entity.ContactKindId == 23)
            {
                entity.ContactTypeSubId = 0;
            }

            return base.Detail(entity);
        }
    }
}