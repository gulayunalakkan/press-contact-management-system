using BasinTakip.Core.Business;
using BasinTakip.Core.Data;
using BasinTakip.Core.Entities.Abstract;
using BasinTakip.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Core.Web
{
    public abstract class BaseController<TManager, TRepository, TEntity, TKey> : Controller
        where TManager : class, IGenericManager<TRepository, TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TRepository : IGenericRepository<TEntity, TKey>
    {
        protected TManager myManager = null;
        public BaseController(TManager manager)
        {
            this.myManager = manager;
        }
        public virtual ActionResult List(GenericListInput<TEntity, TKey> input)
        {
            if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            cookie.Expires = DateTime.Now.AddMinutes(20);
            HttpContext.Response.Cookies.Add(cookie);
            var result = myManager.AllPaged(input.PageNumber, input.PageSize);
            var model = new GenericListOutput<TEntity, TKey>
            {
                SearchText = input.SearchText,
                PagedData = result
            };

            return View(model);
        }
        [HttpGet]
        public virtual ActionResult Detail(DetailInput<TKey> input = null)
        {
            if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            cookie.Expires = DateTime.Now.AddMinutes(20);
            HttpContext.Response.Cookies.Add(cookie);
            var entity = myManager.GetByKey(input.Id);
            if (entity != null)
            {
                ViewBag.DateTime = entity.CreatedAt.ToShortDateString();
                ViewBag.createDate = "Kayıt Tarihi:";
            }
            return View(entity);
        }
        [HttpPost]
        public virtual ActionResult Detail(TEntity entity)
        {
            if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            cookie.Expires = DateTime.Now.AddMinutes(20);
            HttpContext.Response.Cookies.Add(cookie);
            var result = myManager.Save(entity);
            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual ActionResult Details(TEntity entity)
        {
            if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            cookie.Expires = DateTime.Now.AddMinutes(20);
            HttpContext.Response.Cookies.Add(cookie);
            var result = myManager.Save(entity);
            return RedirectToAction("List");

        }

    }
}
