using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace FilmsStorage.Filters
{
    public class LogItAttribute : FilterAttribute, IActionFilter
    {
        private string stringToLog;

        public LogItAttribute()
        {

        }

        public LogItAttribute(string stringToLog)
        {
            this.stringToLog = stringToLog;
        }
        //After method runs
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Debug.WriteLine(stringToLog);
        }

        //Before method runs
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RouteValueDictionary routeInformation = filterContext.RouteData.Values;

            Debug.WriteLine($"Method {routeInformation["controller"]}/{routeInformation["action"]} started");
        }
    }
}