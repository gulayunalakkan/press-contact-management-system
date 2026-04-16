using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Web.Controllers.API
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            cookie.Expires = DateTime.Now.AddMinutes(20);
            HttpContext.Response.Cookies.Add(cookie);

            return View();
        }
    }
}