using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FilmsStorage.Models;
using FilmsStorage.Models.Entities;
using FilmsStorage.DAL;
using FilmsStorage.Mappers;

namespace FilmsStorage.Controllers
{
    public class AccountController : Controller
    {
        // Особистий профіль користувача. Дозвіл на вхід за логіном
        [Authorize]
        // Фільтр вимагає щоб користувач був залогіненим
        // TODO: Замінити штатний фільтр на свій
        public ViewResult Index()
        {
            return View();
        }

        // Вікно реєстрації
        public ViewResult Register()
        {
            return View(new RegisterModel());
        }
        
        // Метод-обробник форми реєстрації
        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                // Перевіримо чи є даний користувач в базі
                User registerUser = _DAL.Users.ByLogin(registerModel.LoginName);

                if(registerUser == null)
                {
                    User userRegisterEntity = UserMapper.FromRegisterModel(registerModel);
                    userRegisterEntity.RegisteredAt = DateTime.Now;

                    User registeredUser = _DAL.Users.Register(userRegisterEntity);

                    // TODO:  create login cookie
                    // TODO:  redirect to profile
                }
                else
                {
                    ViewBag.ErrorMsg = $"User {registerModel.LoginName} is already registered";
                    
                    return View(registerModel);
                }
                
                
                return RedirectToAction("Profile");
            }
            else
            {
                //Помилки у формі реєстрації

                return View(registerModel);
            }
        }
    }
}