using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FilmsStorage.Models;

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
                // TODO: register user
                // create login cookie
                // redirect to profile
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