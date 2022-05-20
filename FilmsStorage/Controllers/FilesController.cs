using FilmsStorage.DAL;
using FilmsStorage.Filters;
using FilmsStorage.Models;
using FilmsStorage.Models.Entities;
using FilmsStorage.SL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        [LogIt("File added")]
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

        public RedirectToRouteResult Delete(int fileID, int userID)
        {
            //TODO: Check if file belongs to current user
            //Potential approach - to use action filter

            Film deletedFilm =_DAL.Films.Delete(fileID);

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

        [LogIt("Show file edit view")]
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Film updatedFilm)
        {
            //TODO: Protect from form manual edition

            if (ModelState.IsValid)
            {
                v_Films filmByID = _DAL.Films.ByID((int)updatedFilm.FilmID);

                if (filmByID.UserID == CurrentUser.UserID)
                {
                    updatedFilm.fk_UserID = CurrentUser.UserID;

                    _DAL.Films.Edit(updatedFilm);

                    return RedirectToAction("Index", "Account");
                }

                TempData["Error"] = "Not successful";

                return View(updatedFilm);
            }
            else
            {
                ViewBag.ErrorMsg = "No file posted";

                ViewData["Genres"] = _DAL.Genres.All();

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

        public JsonResult Details4Js(int fileID)
        {
            var filmByID = _DAL.Films.ByID(fileID);

            //Note: don't use JsonRequestBehavior.AllowGet at prod!
            return Json(filmByID, JsonRequestBehavior.AllowGet);
        }
    }
}