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

namespace BasinTakip.Web.Controllers
{
    public class EventTypeController :BaseController<IPickListManager,IPickListRepository,PickList,int>
    {

        public EventTypeController(IPickListManager manager)
            : base(manager)
        {
        }

        public override ActionResult List(GenericListInput<PickList, int> input)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            return base.List(input);
        }
    }
}