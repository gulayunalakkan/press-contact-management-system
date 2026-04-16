using AutoMapper;
using BasinTakip.Core.Entities.Abstract;
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

namespace BasinTakip.Web.Controllers
{
    public class PickListController : BaseController<IPickListManager, IPickListRepository, PickList, int>
    {
        public PickListController(IPickListManager manager)
            : base(manager)
        {
        }

        public ActionResult Filter(PickListListModel input)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            ViewBag.button = "btn-add";
            ViewBag.BackClass = "viewbag_Back";
            if (input.CategoryId == 1) { ViewBag.Title = "Görev Türleri Yönetimi"; ViewBag.btnNew = "/PickList/Details?CategoryId=1"; }
            if (input.CategoryId == 2) { ViewBag.Title = "Etkinlik Türleri Yönetimi"; ViewBag.btnNew = "/PickList/Details?CategoryId=2"; }
            if (input.CategoryId == 3) { ViewBag.Title = "Temas Türleri Yönetimi"; ViewBag.btnNew = "/PickList/Details?CategoryId=3"; }
            if (input.CategoryId == 4) { ViewBag.Title = "Yayın Türleri Yönetimi"; ViewBag.btnNew = "/PickList/Details?CategoryId=4"; }

            var result = myManager.FilterPaged(p => p.CategoryId == input.CategoryId, input.PageNumber, input.PageSize);
          
            var model = new GenericListOutput<PickList, int>
            {
                SearchText = input.SearchText,
                PagedData = result
            };

            return View("List", model);
        }
        public  ActionResult Details(PickListListModel entity)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            switch (entity.CategoryId)
            {
                case 1:
                    ViewBag.BackClass = "vievbag_detail";
                    ViewBag.Back = "/PickList/Filter?CategoryId=1"; break;
                case 2:
                    ViewBag.BackClass = "vievbag_detail";
                    ViewBag.Back = "/PickList/Filter?CategoryId=2"; break;
                case 3:
                    ViewBag.BackClass = "vievbag_detail";
                    ViewBag.Back = "/PickList/Filter?CategoryId=3"; break;
                case 4:
                    ViewBag.BackClass = "vievbag_detail";
                    ViewBag.Back = "/PickList/Filter?CategoryId=4"; break;
                default:
                    ViewBag.BackClass = "vievbag_detail";
                    ViewBag.Back = "/Home/Index"; break;
            }
           
            var result = new PickList();

            var pickListManager = IocManager.Resolve<IPickListCategoryManager>();
            var pickListCategoryList = pickListManager.Filter(x => x.Id == 1 || x.Id == 2 || x.Id == 4 && x.IsDeleted==false);

            var pickList = myManager.All();
            var model = Mapper.Map<PickListDetailModel>(result);
           
            model.PickListCategoryList = pickListCategoryList.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == entity.CategoryId
            }).ToList();
          
            return View("Detail",model);

        }
        public override ActionResult Detail(DetailInput<int> input = null)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            ViewBag.BackClass = "vievbag_detail";
            
            var entity = myManager.GetByKey(input.Id);
           
            ViewBag.Back = "/Home/Index";
            if (entity == null) entity = new PickList();

            var pickListManager = IocManager.Resolve<IPickListCategoryManager>();
            var pickListCategoryList = pickListManager.Filter(x=>x.Id==1 || x.Id==2 || x.Id==4 && x.IsDeleted==false);

            var pickList = myManager.All();
            var model = Mapper.Map<PickListDetailModel>(entity);

            switch (entity.CategoryId)
            {
                case 1:
                    ViewBag.BackClass = "vievbag_detail";
                    ViewBag.Back = "/PickList/Filter?CategoryId=1"; break;
                case 2:
                    ViewBag.BackClass = "vievbag_detail";
                    ViewBag.Back = "/PickList/Filter?CategoryId=2"; break;
                case 3:
                    ViewBag.BackClass = "vievbag_detail";
                    ViewBag.Back = "/PickList/Filter?CategoryId=3"; break;
                case 4:
                    ViewBag.BackClass = "vievbag_detail";
                    ViewBag.Back = "/PickList/Filter?CategoryId=4"; break;
                default:
                    ViewBag.BackClass = "vievbag_detail";
                    ViewBag.Back = "/Home/Index"; break;
            }
            if (entity != null)
            {
                ViewBag.DateTime = model.CreatedAt.ToShortDateString();
                ViewBag.createDate = "Kayıt Tarihi:";
            }
            model.PickListCategoryList = pickListCategoryList.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == entity.CategoryId
            }).ToList();
           
            return View(model);
        }
        public ActionResult Delete(int Id)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            PickListDetailModel input = new PickListDetailModel();
            input.CategoryId = myManager.Filter(x => x.Id == Id).SingleOrDefault().CategoryId;
            myManager.DeleteByKey(Id);
            return RedirectToAction("Filter","PickList",input);
        }
    }
}