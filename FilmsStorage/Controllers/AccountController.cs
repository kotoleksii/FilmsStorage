using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using FilmsStorage.Models;
using FilmsStorage.Models.Entities;
using FilmsStorage.DAL;
using FilmsStorage.Mappers;
using FilmsStorage.SL;

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

        #region Login
        // Вікно логіну
        public ViewResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Перевіряємо наявність користувача в базі
                User registeredUser = _DAL.Users.ByLogin(loginModel.LoginName);

                //if user registered
                if (registeredUser != null)
                {
                    //Password check
                    bool isPasswordValid = _SL.Hasher.checkPassword(registeredUser.Password, loginModel.Password);

                    if (isPasswordValid)
                    {
                        _SL.Cookies.SetLoginCookie(registeredUser);

                        return RedirectToAction("Index", "Account");
                        //TODO: create login cookie
                    }
                    else
                    {
                        ViewBag.ErrorMsg = "Wrong Password";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErrorMsg = "No Such User";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Invalid Login Form";
                return View();
            }
        }
        #endregion

        public RedirectToRouteResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        #region Register
        // Вікно реєстрації
        public ViewResult Register()
        {
            return View(new RegisterModel());
        }
        
        // Метод-обробник форми реєстрації
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                // Перевіримо чи є даний користувач в базі
                User registerUser = _DAL.Users.ByLogin(registerModel.LoginName);

                // Такого користувача немає - можна додавати
                if(registerUser == null)
                {
                    User userRegisterEntity = UserMapper.FromRegisterModel(registerModel);
                    userRegisterEntity.RegisteredAt = DateTime.Now;

                    User registeredUser = _DAL.Users.Register(userRegisterEntity);

                    // TODO:  create login cookie
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ViewBag.ErrorMsg = $"User {registerModel.LoginName} is already registered";
                    
                    return View(registerModel);
                }
            }
            else
            {
                //Помилки у формі реєстрації
                ViewBag.ErrorMsg = "Invalid Register Form";
                return View(registerModel);
            }
        }
        #endregion 


    }
}