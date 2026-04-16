using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Entities.Common;
using BasinTakip.Domain.Manager;
using BasinTakip.EntityFramework.Repository;
using BasinTakip.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace BasinTakip.Web.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
          #if DEBUG
            // LOCAL/DEBUG: SSO'ya gitme, login sayfasını aç
            ViewBag.ReturnUrl=returnUrl;
            return View();
          #endif
         
            
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (Session["captcha"] != null && model.Captcha == Session["captcha"].ToString())
            {
                ModelState.AddModelError("", "Doğrulama kodunu doğru girdiniz.");


            }
            else
            {
                ModelState.AddModelError("", "Doğrulama kodunu yanlış girdiniz.");
                return View(model);
            }


            using (var repository = new LoginLogRepository())
            {
                if (repository.All().Where(x => x.UserName == model.UserName && x.LastLoginDate >= DateTime.Now.AddDays(-1)).Count() >= 5)
                {
                    using (var Repository = new LoginLogRepository())
                    {
                        LoginLog loginlog = new LoginLog();
                        loginlog.UserName = model.UserName;
                        loginlog.LastLoginDate = DateTime.Now;
                        loginlog.IsActive = false;
                        loginlog.Description = "Hatalı Giriş";
                        Repository.Save(loginlog);
                    }
                    ModelState.AddModelError(string.Empty, "Kullanıcı pasifleştirildi.");
                    //   return RedirectToAction("Pages_404", "Error");
                    return View(model);
                    //  return Redirect("/Error/Pages_404");
                }
            }
            var manager = IocManager.Resolve<IAccountManager>();

            var loginResult = manager.Login(model.UserName, model.Password);
            string user = manager.GetLoginName(model.UserName);
            switch (loginResult)
            {
                case LoginResponseType.UserInformationFailed:
                    ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre bilgisi yanlış");
                    var test = IocManager.Resolve<IFirmManager>();
                    using (var Repository = new LoginLogRepository())
                    {
                        LoginLog loginlog = new LoginLog();
                        loginlog.UserName = model.UserName;
                        loginlog.LastLoginDate = DateTime.Now;
                        loginlog.IsActive = false;
                        loginlog.Description = "Hatalı Giriş";
                        Repository.Save(loginlog);
                    }
                     return View(model);
                 // return RedirectToAction("login", "account", new { login = "ok" });
                case LoginResponseType.GroupAuthorizationFailed:
                    ModelState.AddModelError(string.Empty, "Kullanıcının portala erişim yetkisi yoktur");
                    return View(model);
                case LoginResponseType.Succeed:
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    HttpCookie cookie = new HttpCookie("login", user);
                    cookie.Expires = DateTime.Now.AddMinutes(20);
                    HttpContext.Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                default:
                    throw new NotImplementedException();
            }
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }


        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            int i, r, x, y;
            var rand = new Random((int)DateTime.Now.Ticks);
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);
            Session["Captcha"] = a + b;
            FileContentResult img = null;
            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));
                if (noisy)
                {
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb((rand.Next(0, 255)), (rand.Next(0, 255)), (rand.Next(0, 255)));
                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);
                    }
                }
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }
            return img;
        }


        public CaptchaResult capthaGetir()
        {
            return new CaptchaResult();
        }
    }
    
   

}