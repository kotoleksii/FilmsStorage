using FilmsStorage.DAL;
using FilmsStorage.Models;
using FilmsStorage.Models.Entities;
using FilmsStorage.SL;
using System;
using System.Web.Mvc;

namespace FilmsStorage.Controllers
{
    [Authorize]
    public class FilesController : BaseController
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
            if (Request.Files[0].ContentLength > 0)
            {
                FileSaveResult fileInfo = _SL.Files.SaveFilm(Request.Files[0], CurrentUser.UserID);

                if (fileInfo.isSaved)
                {
                    addFilmModel.UserID = CurrentUser.UserID;
                    addFilmModel.FilePath = fileInfo.FilePath;
                    addFilmModel.FileName = fileInfo.FileName;

                    Film addedFilm = _DAL.Films.Add(addFilmModel);

                    //if film added
                    if(addedFilm.FilmID > 0)
                    {
                        return RedirectToAction("Index", "Account");
                    }
                    else
                    {
                        //TODO: Delete file
                        //TODO: Show Error
                    }
                }

                return View(addFilmModel);
            }
            else
            {
                ViewBag.ErrorMsg = "No file posted";

                addFilmModel.Genres = _DAL.Genres.All();

                return View(addFilmModel);
            }
        }
    }
}