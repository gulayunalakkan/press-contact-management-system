using BasinTakip.Core.Web;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasinTakip.Core.Entities.Abstract;
using BasinTakip.Domain.Repository;
using AutoMapper;
using BasinTakip.Web.Models;

namespace BasinTakip.Web.Controllers
{
    public class VehicleController :BaseController<IVehicleManager,IVehicleRepository,Vehicle,int>
    {
        public VehicleController(IVehicleManager manager)
            :base(manager)
        {

        }
        public override ActionResult List(GenericListInput<Vehicle, int> input)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            var result = myManager.GetInclueded(input.PageNumber, input.PageSize, input.OrderByColumn ?? "Id", input.OrderType, input.SearchText,input.ModelYear);
            
            var model = new GenericListOutput<Vehicle, int>
            {
                SearchText = input.SearchText,
                PagedData = result,

            };
            return View(model);
        }
        public override ActionResult Detail(DetailInput<int> input = null)
        {
            if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            cookie.Expires = DateTime.Now.AddMinutes(20);
            HttpContext.Response.Cookies.Add(cookie);
            ViewBag.BackClass = "vievbag_detail";
            ViewBag.Back = "/Event/List";
            var entity = myManager.GetByKey(input.Id);
            if (entity != null)
            {
                ViewBag.DateTime = entity.CreatedAt.ToString("dd/MM/yyyy");
                ViewBag.createDate = "Kayıt Tarihi:";
            }
            if (entity == null) { entity = new Vehicle(); ViewBag.Export = "display:none;"; }

                var editionManager = IocManager.Resolve<IPickListManager>();
            var editionTypeList = editionManager.Filter(x => x.CategoryId == 2 && x.IsDeleted == false);

            var model = Mapper.Map<VehicleDetailModel>(entity);
            model.ContactRecordBackList = myManager.GetFilterContactRecord(model.Id);
           
            return View(model);
        }
        public ActionResult Delete(int Id)
        {
            if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            cookie.Expires = DateTime.Now.AddMinutes(20);
            HttpContext.Response.Cookies.Add(cookie);
            myManager.DeleteByKey(Id);
            return RedirectToAction("List");
        }
    }
}