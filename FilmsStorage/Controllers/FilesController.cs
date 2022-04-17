using FilmsStorage.DAL;
using FilmsStorage.Models;
using System;
using System.Web.Mvc;

namespace FilmsStorage.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        // GET: Files By User
        public PartialViewResult ByUser()
        {
            //TODO: Get Files Of Given User

            return PartialView();
        }

        //Show add file form
        public ViewResult Add()
        {
            FilmAddModel addFilmModel = new FilmAddModel();

            addFilmModel.ReleaseYear = DateTime.Now.Year;
            addFilmModel.Genres = _DAL.Genres.All();

            return View(addFilmModel);
        }

        [HttpPost]
        public ActionResult Add(FilmAddModel addFilmModel)
        {
            if(Request.Files[0].ContentLength > 0)
            {
                return View(addFilmModel);
            }
            else
            {
                ViewBag.ErrorMsg = "No file posted";

                return View(addFilmModel);
            }
        }
    }
}