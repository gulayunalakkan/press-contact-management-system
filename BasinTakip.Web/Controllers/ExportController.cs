using BasinTakip.Application;
using BasinTakip.Domain.Entities.Abstract;
using BasinTakip.Domain.Manager;
using BasinTakip.Domain.Repository;
using BasinTakip.EntityFramework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Web.Controllers
{
    public class ExportController : Controller
    {
        [HttpGet]
        public ActionResult PressMemberReport(PersonResult input)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            var exportResult = BasınTakipManager.Export.PersonReport(input);

            var fileName = string.Format("BasınMensubuRaporu_{0:ddMMyyyy_HHmm}.xlsx", DateTime.Now);

            return File(exportResult, "application/vnd.ms-excel", fileName);
        }

        [HttpGet]
        public ActionResult ContactReport(ContactRecordResult input)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            var exportResult = BasınTakipManager.Export.ContactReport(input);

            var fileName = string.Format("TemasKayıtlarıRaporu_{0:ddMMyyyy_HHmm}.xlsx", DateTime.Now);

            return File(exportResult, "application/vnd.ms-excel", fileName);
        }

        [HttpGet]
        public ActionResult EventReport(ReportInput input)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            var exportResult = BasınTakipManager.Export.EventReport(input);

            var fileName = string.Format("EtkinlikRaporu_{0:ddMMyyyy_HHmm}.xlsx", DateTime.Now);

            return File(exportResult, "application/vnd.ms-excel", fileName);
        }

        [HttpGet]
        public ActionResult VehicleReport()
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            var exportResult = BasınTakipManager.Export.VehicleReport();

            var fileName = string.Format("TestAraçlarıRaporu_{0:ddMMyyyy_HHmm}.xlsx", DateTime.Now);

            return File(exportResult, "application/vnd.ms-excel", fileName);
        }

        [HttpGet]
        public ActionResult EditionReport(EditionResult input)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            var exportResult = BasınTakipManager.Export.EditionReport(input);

            var fileName = string.Format("YayınRaporu_{0:ddMMyyyy_HHmm}.xlsx", DateTime.Now);

            return File(exportResult, "application/vnd.ms-excel", fileName);
        }

        [HttpGet]
        public ActionResult ContactBackPressmemberReport(int id)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            var exportResult = BasınTakipManager.Export.ContactBackPressmemberReport(id);
            var test = IocManager.Resolve<IPersonManager>().Filter(x => x.Id == id).SingleOrDefault();
            var PersonName = test.FirstName+" "+test.LastName;

            var fileName = string.Format(PersonName+"_TemasGeçmişiRaporu_{0:ddMMyyyy_HHmm}.xlsx", DateTime.Now);

            return File(exportResult, "application/vnd.ms-excel", fileName);
        }

        [HttpGet]
        public ActionResult ContactBackEventReport(int id)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            var exportResult = BasınTakipManager.Export.ContactBackEventReport(id);
            var test = IocManager.Resolve<IEventManager>().Filter(x => x.Id == id).SingleOrDefault();
            var EventName = test.Name;

            var fileName = string.Format(EventName+ "_TemasGeçmişiRaporu_{0:ddMMyyyy_HHmm}.xlsx", DateTime.Now);

            return File(exportResult, "application/vnd.ms-excel", fileName);
        }

        [HttpGet]
        public ActionResult ContactBackVehicleReport(int id)
        {
            //if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            //Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            //HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            //cookie.Expires = DateTime.Now.AddMinutes(20);
            //HttpContext.Response.Cookies.Add(cookie);
            var exportResult = BasınTakipManager.Export.ContactBackVehicleReport(id);
            var test = IocManager.Resolve<IVehicleManager>().Filter(x => x.Id == id).SingleOrDefault();
            var VehicleName = test.Serial + " " + test.Model;

            var fileName = string.Format(VehicleName+"_TemasGeçmişiRaporu_{0:ddMMyyyy_HHmm}.xlsx", DateTime.Now);

            return File(exportResult, "application/vnd.ms-excel", fileName);
        }
    }
}