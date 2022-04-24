using FilmsStorage.Models.Login;
using System.Web.Mvc;

namespace FilmsStorage.Controllers
{
    public class BaseController : Controller
    {
        protected new CustomPrincipal CurrentUser
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}