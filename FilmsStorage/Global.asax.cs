using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;


namespace FilmsStorage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //TODO: Find event fires only if auth does exist
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie loginCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if(loginCookie != null)
            {
                FormsAuthenticationTicket originalAuthTicket = FormsAuthentication.Decrypt(loginCookie.Value);

                int cookieTimeoutMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["LoginTimeout"]);

                FormsAuthenticationTicket prolongatedAuthTicket = new FormsAuthenticationTicket(
                    version: 1,
                    name: originalAuthTicket.Name,
                    issueDate: DateTime.Now,
                    expiration: DateTime.Now.AddMinutes(cookieTimeoutMinutes),
                    isPersistent: true,
                    userData: originalAuthTicket.UserData
                    );
                //TODO: Refactor as reusable function
                string encryptedAuthTicket = FormsAuthentication.Encrypt(prolongatedAuthTicket);

                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedAuthTicket);

                //Cookie lifetime should be no longer than auth ticket lifetime
                authCookie.Expires = prolongatedAuthTicket.Expiration;

                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }
    }
}
