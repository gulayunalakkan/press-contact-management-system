using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    public static class Extensions
    {
        public static MvcHtmlString ToRelativeUri(this HtmlHelper htmlHelper, string uri)
        {
            //string result = "";
            //if (uri != null)
            //{
            //    string ext = uri.Replace("/Upload/", "/");
            //    string test = Path.GetDirectoryName("/Upload");
            //    result = "/Upload" + ext;
            //}
            //else
            //{
            //    result = "/Upload/Default.png";
            //}

            if (string.IsNullOrEmpty(uri))
            {
                uri = "/upload/default.png";
            }

            return MvcHtmlString.Create(uri);
        }
    }
}
