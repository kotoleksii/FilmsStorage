using FilmsStorage.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmsStorage.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ViewResult Index()
        {
            return View();
        }

        //TODO: Grant "userID = 1" access only
        [RootUserOnly]
        public ViewResult Test()
        {
            return View();
        }

        [Route("RouteTest/a/{a}/b/{b}")]
        public RedirectToRouteResult RouteTest(int a, int b)
        {
            return RedirectToAction("Index");
        }
    }
}