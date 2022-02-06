using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FilmsStorage.Models.Entities;
using FilmsStorage.Models;

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
        
        }
    }
}