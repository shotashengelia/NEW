using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Metra_Development.Controllers
{
    public class BaseController : Controller
    {
        protected  string Language { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.RouteData.Values.ContainsKey("language"))
            {
                Language = filterContext.RouteData.Values["language"].ToString().ToLower();
            }
            else
                Language = "ka-ge";

            ViewBag.language = Language;

            Thread.CurrentThread.CurrentCulture = new CultureInfo(Language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Language);
        }
    }
}