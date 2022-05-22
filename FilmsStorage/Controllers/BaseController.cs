using FilmsStorage.Models.Login;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Configuration;
using System.Linq;
using System.Web;
using System;

namespace FilmsStorage.Controllers
{
    public class BaseController : Controller
    {
        protected CustomPrincipal CurrentUser
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

        private string[] availableCultures = new string[]
        {
            "en", "uk", "es", "de"
        };

        private const string DEFAULT_LANG = "uk";

        private string _webSiteLang = DEFAULT_LANG;

        public string webSiteLang {
            get { return _webSiteLang; }
            set {
                if(availableCultures.Contains(value))
                    _webSiteLang = value;
            }
        } 

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string langFromRoute = RouteData.Values["lang"]?.ToString();

            if (!string.IsNullOrEmpty(langFromRoute))
            {
                if (availableCultures.Contains(langFromRoute))
                    webSiteLang = langFromRoute;
            }
            else
            {
                HttpCookie langCookie = Request.Cookies["WebSiteLang"];

                if (langCookie == null)
                {
                    string defaultWebSiteLang = ConfigurationManager.AppSettings["DefaultWebSiteLang"];

                    if (!string.IsNullOrEmpty(defaultWebSiteLang))
                    {
                        webSiteLang = defaultWebSiteLang;
                    }

                    Response.Cookies.Add(new HttpCookie("WebSiteLang", webSiteLang) { Expires = DateTime.MaxValue });
                }
                else
                {
                    webSiteLang = langCookie.Value;
                }
            }          

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(webSiteLang);
        }
    }
}