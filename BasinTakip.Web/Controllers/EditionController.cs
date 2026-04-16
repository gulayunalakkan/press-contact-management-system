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
using BasinTakip.Web.Models;
using AutoMapper;
using BasinTakip.Domain.Entities.Abstract;

namespace BasinTakip.Web.Controllers
{
    public class EditionController : BaseController<IEditionManager, IEditionRepository, Edition, int>
    {
        public EditionController(IEditionManager manager) 
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
            ViewBag.BackClass = "vievbag_detail";
            ViewBag.Back = "/Edition/List";
            var entity = myManager.GetByKey(input.Id);
            if (entity != null)
            {
                ViewBag.DateTime =entity.CreatedAt.ToString("dd/MM/yyyy");
                ViewBag.createDate = "Kayıt Tarihi:";
            }
            if (entity == null) entity = new Edition();
            
            var editionManager = IocManager.Resolve<IPickListManager>();
            var editionTypeList = editionManager.Filter(x=>x.CategoryId== 4 && x.IsDeleted == false);
           

            var model = Mapper.Map<EditionDetailModel>(entity);
            using (IocManager.BeginScope())
            {
                var editionRepository = IocManager.Resolve<IEditionRepository>();
                model.EditionWithPressMember = editionRepository.PastContactEditionWithPress(input.Id);
            }
              
            model.EditionTypeList = editionTypeList.OrderBy(x=>x.Name).Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == entity.EditionTypeId
            }).ToList();
      
            return View(model);
        }

        public override ActionResult List(GenericListInput<Edition, int> input)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            ViewBag.btnNew = "/Edition/Detail";
            ViewBag.button = "btn-add";
            ViewBag.BackClass = "viewbag_Back";
            ViewBag.QueryString ="&SearchText=" + input.SearchText + "&OrderByColumn=" + input.OrderByColumn + "&OrderType=" + input.OrderType;
            var result = myManager.GetInclueded(input.PageNumber, input.PageSize, input.OrderByColumn ?? "Id", input.OrderType, input.SearchText,input.EditionId,input.EditionTypeId);
            ViewBag.EditionList = myManager.All().OrderBy(x=>x.Name);
            var pickListManager = IocManager.Resolve<IPickListManager>();
            ViewBag.EditionTypeList = pickListManager.Filter(x => x.CategoryId ==4 && x.IsDeleted==false).OrderBy(x=>x.Name);
            var model = new GenericListOutput<EditionResult, int>
            {
                SearchText = input.SearchText,
                PagedData = result
            };

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
    }
}