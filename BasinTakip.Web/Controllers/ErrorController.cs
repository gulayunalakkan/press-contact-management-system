using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Pages_404()
        {
            return View();
        }
    }
}