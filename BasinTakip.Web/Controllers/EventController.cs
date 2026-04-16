using AutoMapper;
using BasinTakip.Core.Web;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Manager;
using BasinTakip.Domain.Repository;
using BasinTakip.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasinTakip.Core.Entities.Abstract;
using BasinTakip.Domain.Entities.Abstract;

namespace BasinTakip.Web.Controllers
{
    public class EventController : BaseController<IEventManager, IEventRepository, Event, int>
    {
        public EventController(IEventManager manager)
            : base(manager)
        {
        }
        public override ActionResult Detail(DetailInput<int> input = null)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            //ViewBag.BackClass = "vievbag_detail";
            ViewBag.Back = "/Event/List";
            var entity = myManager.GetByKey(input.Id);
            if (entity != null)
            {
                ViewBag.DateTime = entity.CreatedAt.ToString("dd/MM/yyyy");
                ViewBag.createDate = "Kayıt Tarihi:";
            }
            if (entity == null) { entity = new Event(); ViewBag.Export = "display:none;"; }

                var editionManager = IocManager.Resolve<IPickListManager>();
            var editionTypeList = editionManager.Filter(x => x.CategoryId == 2 && x.IsDeleted==false);

            var model = Mapper.Map<EventDetailModel>(entity);
            model.ContactRecordBackList = myManager.GetFilterContactRecord(model.Id);
            model.EventTypeList = editionTypeList.OrderBy(x=>x.Name).Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == entity.EventTypeId
            }).ToList();
           
            return View(model);
        }

        public override ActionResult List(GenericListInput<Event, int> input)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            ViewBag.btnNew = "/Event/Detail";
            ViewBag.button = "btn-add";
            ViewBag.BackClass = "viewbag_Back";
            ViewBag.QueryString = "&BeginYear=" + input.BeginYear + "&BeginMonth=" + input.BeginMonth + "&SearchText=" + input.SearchText + "&OrderByColumn=" + input.OrderByColumn + "&OrderType=" + input.OrderType+"&EventPlace="+input.EventPlace+"&EventType="+input.EventType;
            if (input.BeginYear != null & input.BeginMonth != null)
            {
                    input.BeginDate = new DateTime((int)input.BeginYear, (int)input.BeginMonth, 1);
                
            }
            var eventmanager = IocManager.Resolve<IEventManager>();
            var eventList = eventmanager.Filter(p => p.IsDeleted == false).OrderBy(x=>x.EventPlace).Select(x=>x.EventPlace).Distinct();
            
            var picklistManager = IocManager.Resolve<IPickListManager>();
            var eventTYpeList = picklistManager.Filter(x => x.CategoryId == 2 && x.IsDeleted == false).OrderBy(x=>x.Name);
            ViewBag.eventList = eventList;
            ViewBag.eventTypeList = eventTYpeList;
            var result = myManager.GetInclueded(input.PageNumber,input.PageSize, input.BeginDate, input.EventPlace, input.EventType, input.OrderByColumn ?? "Id", input.OrderType, input.SearchText,input.BeginYear,input.BeginMonth);
            string[] filter=new string[9];
            filter[0] =input.BeginYear.ToString() ;
            filter[1] = input.EndYear.ToString();
            filter[2] = input.EventPlace;
            filter[3] = input.EventType.ToString();
            filter[4] = input.SearchText;
            filter[5] = input.BeginMonth.ToString();
            filter[6] = input.Endmonth.ToString();
            filter[7] = input.OrderByColumn;
            filter[8] = input.OrderType.ToString();
            var model = new GenericListOutput<EventResult, int>
            {
                SearchText = input.SearchText,
                PagedData = result,
                filter = filter  
                
            };
            if (result==null)
            {
               
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
            var inputs= myManager.Filter(x => x.Id == Id).SingleOrDefault();
                      //  myManager.Delete(inputs);
            myManager.DeleteByKey(Id);
            return RedirectToAction("List");
        }

        public ActionResult Calendar()
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            return View();
        }

        public override ActionResult Detail(Event entity)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            var result = myManager.Save(entity);
            entity.Url = "/Event/Detail?id=" + entity.Id;
            result = myManager.Save(entity);
            return RedirectToAction("List");
        }
    }
}