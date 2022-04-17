using System.Web.Mvc;

namespace FilmsStorage.Controllers
{
    public class FilesController : Controller
    {
        // GET: Files By User
        public PartialViewResult ByUser()
        {
            //TODO: Get Files Of Given User

            return PartialView();
        }
    }
}