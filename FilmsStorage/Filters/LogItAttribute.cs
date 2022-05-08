using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Filters;
using System.Web.Mvc;
using System.Diagnostics;

namespace FilmsStorage.Filters
{
    public class LogItAttribute : FilterAttribute, IActionFilter
    {
        private string stringToLog;
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
        }
    }
}