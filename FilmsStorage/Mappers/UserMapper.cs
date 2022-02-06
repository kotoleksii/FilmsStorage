using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilmsStorage.Models.Entities;
using FilmsStorage.Models;
using FilmsStorage.SL;

namespace FilmsStorage.Mappers
{
    public static class UserMapper
    {
        public static User FromRegisterModel(RegisterModel registerModel)
        {
            return new User()
            {
                UserName = registerModel.UserName,
                Login = registerModel.LoginName,
                // TODO: rework to create hash outside
                Password = _SL.Hasher.createHash(registerModel.Password)
            };
        }
    }
}