using FilmsStorage.DAL;
using FilmsStorage.Models;
using FilmsStorage.Models.Entities;
using FilmsStorage.SL;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FilmsStorage.Controllers
{
    [Authorize]
    public class FilesController : BaseController
    {
        // GET: Files By User
        public PartialViewResult FilmsByUser()
        {
            //TODO: Get Files Of Given User
            List<v_Films> userFilms = _DAL.Films.ByUser(CurrentUser.UserID);

            return PartialView(userFilms);
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

        public RedirectToRouteResult Delete(int id)
        {
            //TODO: Check if file belongs to current user
            //Potential approach - to use action filter

            Film deletedFilm =_DAL.Films.Delete(id);

            if(deletedFilm != null)
            {
                bool isFlmDeleted = _SL.Files.DeleteFilm(deletedFilm);

                if (!isFlmDeleted)
                {
                    TempData["Error"] = "Error deliting Film file from file system";
                }
            }
            else
            {
                TempData["Error"] = "No such file to delete";
            }

            return RedirectToAction("Index", "Account"); 
        }

        public ActionResult Edit(int id)
        {
            //TODO: Check if file belongs to current user

            var filmByID = _DAL.Films.FilmByID(id);

            if (filmByID != null)
            {
                ViewData["Genres"] = _DAL.Genres.All();

                return View(filmByID);
            }
            else
            {
                TempData["Error"] = "No such film";

                return RedirectToAction("Index", "Account");
            }
        }

        [HttpPost]
        public ActionResult Edit(Film updatedFilm)
        {
            //TODO: Protect from form manual edition

            if (ModelState.IsValid)
            {
                //TODO: Завершити реалізацію методу Edit
                //Подумати на тим як можна захистити проєкт від "угону" чужих файлів

                return RedirectToAction("Index", "Account");
            }
            else
            {
                return View(updatedFilm);
            }
        }

        public ActionResult Details(int id)
        {
            //TODO: Check if file belongs to current user

            var filmByID = _DAL.Films.ByID(id);

            if(filmByID != null)
            {
                return View(filmByID);
            }
            else
            {
                TempData["Error"] = "No such film";

                return RedirectToAction("Index", "Account");
            }
        }
    }
}