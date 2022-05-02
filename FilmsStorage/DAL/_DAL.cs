using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FilmsStorage.Models.Entities;
using FilmsStorage.Models;
using FilmsStorage.Mappers;

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

            public static int Delete(int filmID)
            {
                int countOfDeletedFiles = 0;

                using (var db = new FilmsStorageDB())
                {
                    var searchResult = db.Films.Where(f => f.FilmID == filmID);

                    if (searchResult.Any())
                    {
                        Film filmToDelete = searchResult.First();

                        db.Films.Remove(filmToDelete);
                        countOfDeletedFiles = db.SaveChanges();
                    }
                }

                return countOfDeletedFiles;
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