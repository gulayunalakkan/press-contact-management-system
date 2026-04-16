using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Web.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            cookie.Expires = DateTime.Now.AddMinutes(20);
            HttpContext.Response.Cookies.Add(cookie);
            string query = HttpContext.Request.Url.Query;
            string elementId = query.Replace("?elementId=p", "P");
            ViewBag.ElementId = elementId;

            ViewBag.IsFileUploaded = "false";
            ViewBag.FilePath = string.Empty;
            return View();
        }
        [HttpPost]
        public ActionResult Save(HttpPostedFileBase file1, string elementId)
        {
            if (HttpContext.Request.Cookies["login"] == null) return RedirectToAction("login", "account");
            Response.Cookies["login"].Expires = DateTime.Now.AddMinutes(-20);
            HttpCookie cookie = new HttpCookie("login", HttpContext.Request.Cookies["login"].Value);
            cookie.Expires = DateTime.Now.AddMinutes(20);
            HttpContext.Response.Cookies.Add(cookie);
            ViewBag.IsFileUploaded = "true";
            ViewBag.FilePath = string.Empty;
            ViewBag.ElementId = elementId;

            if (file1 != null)
            {
         bool isImage = false;

        isImage = IsImage(file1);

         if (isImage)
            {


                    string uzanti = Path.GetExtension(file1.FileName);
                    if (uzanti.ToLower() == ".jpg" || uzanti.ToLower() == ".png" || uzanti.ToLower() == ".jpeg" || uzanti.ToLower() == ".gif")
                    {
                        var uploadPath = ConfigurationManager.AppSettings["UploadPath"];

                        if (uploadPath.StartsWith("/"))
                        {
                            uploadPath = Server.MapPath(uploadPath);
                        }

                        var newFileName = Guid.NewGuid().ToString().ToLower() + Path.GetExtension(file1.FileName);
                        var newFilePath = Path.Combine(uploadPath, newFileName);

                        ViewBag.FilePath = "/upload/" + newFileName;

                        file1.SaveAs(newFilePath);
                    }
                    else
                    {
                        ViewBag.FilePath = "/upload/" + "Default.png";
                    }
                    return View("Index");
                }
                else
                {
                    ViewBag.FilePath = "/upload/" + "Default.png";
                }
            }
            return View("Index");
        }


        public const int ImageMinimumBytes = 512;

        public bool IsImage(HttpPostedFileBase postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() !=
"image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() !=
"image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() !=
".png"
                && Path.GetExtension(postedFile.FileName).ToLower() !=
".gif"
                && Path.GetExtension(postedFile.FileName).ToLower() !=
".jpeg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    return false;
                }

                byte[] buffer = new byte[ImageMinimumBytes];
                postedFile.InputStream.Read(buffer, 0, ImageMinimumBytes);
                string content =
System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content,
@"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase |
RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new
System.Drawing.Bitmap(postedFile.InputStream))
                {

                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                postedFile.InputStream.Position = 0;
            }

            return true;
        }

    }
}