using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FilmsStorage.Models.Entities;
using FilmsStorage.Models;
using FilmsStorage.Mappers;
using System.Data.Entity;

namespace FilmsStorage.DAL
{
    public static class _DAL
    {
        public static class Users {
            public static User ByLogin(string loginName)
            {
                using (var db = new FilmsStorageDB())
                {
                    User registeredUser = null;

                    // TODO: rework without checking on exception
                    try
                    {
                        registeredUser = db.Users.Where(u => u.Login == loginName).First();
                    }
                    catch (InvalidOperationException)
                    { }

                    return registeredUser;
                }
            }

            public static User Register(User registerModel)
            {
                using (var db = new FilmsStorageDB())
                {
                    db.Users.Add(registerModel);
                    db.SaveChanges();
                }

                return registerModel;
            }
        }


        public static class Films {

            public static Film Add(FilmAddModel addFilmModel)
            {
                using(var db = new FilmsStorageDB())
                {
                    Film filmToAdd = FilmMapper.FromAddModel(addFilmModel);

                    db.Films.Add(filmToAdd);
                    db.SaveChanges();

                    return filmToAdd;
                }
            }

            public static Film Delete(int filmID)
            {
                Film deletedFile = null;

                using (var db = new FilmsStorageDB())
                {
                    var searchResult = db.Films.Where(f => f.FilmID == filmID);

                    if (searchResult.Any())
                    {
                        Film filmToDelete = searchResult.First();

                        deletedFile = db.Films.Remove(filmToDelete);
                        db.SaveChanges();
                    }
                }

                return deletedFile;
            }

            public static List<v_Films> ByUser(int userID)
            {
                List<v_Films> userFilms = new List<v_Films>();

                using (var db = new FilmsStorageDB())
                {
                    var searchResults = db.v_Films.Where(f => f.UserID == userID);

                    if (searchResults.Any())
                    {
                        userFilms = searchResults.ToList();
                    }
                }
                return userFilms;
            }

            public static Film FilmByID(int filmID)
            {
                Film filmByID = null;

                using (var db = new FilmsStorageDB())
                {
                    var searchResults = db.Films.Where(f => f.FilmID == filmID);

                    if (searchResults.Any())
                    {
                        filmByID = searchResults.First();
                    }
                }
                return filmByID;
            }

            public static v_Films ByID(int filmID)
            {
                v_Films filmByID = null;

                using (var db = new FilmsStorageDB())
                {
                    //Turn LazyLoading off to allow JSON serialization
                    //DB Record will be returned immediately
                    db.Configuration.LazyLoadingEnabled = false;

                    var searchResults = db.v_Films.Where(f => f.FilmID == filmID);

                    if (searchResults.Any())
                    {
                        filmByID = searchResults.First();
                    }
                }
                return filmByID;
            }
        }

        public static class Genres
        {
            public static List<Genre> All()
            {
                using (var db = new FilmsStorageDB())
                {
                    return db.Genres.ToList();
                }
            }
        }
    }
}