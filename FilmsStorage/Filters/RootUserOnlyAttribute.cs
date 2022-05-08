using FilmsStorage.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace FilmsStorage.Filters
{
    public class RootUserOnlyAttribute : ActionFilterAttribute, IAuthorizationFilter
    {     
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            RedirectToRouteResult redirectToRoute = new RedirectToRouteResult("ShowError", null);

            //TODO: Pass error text to show by /Home/Error

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = redirectToRoute;
            }
            else if ((filterContext.HttpContext.User as CustomPrincipal).UserID != 1)
            {
                filterContext.Result = redirectToRoute;
            }
        }     
    }
}