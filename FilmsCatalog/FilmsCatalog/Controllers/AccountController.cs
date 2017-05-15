using FilmsCatalog.Models;
using FilmsCatalog.Models.DomainModels;
using FilmsCatalog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FilmsCatalog.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (DatabaseContext db = new DatabaseContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Name == model.Name && u.Password == model.Password);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (DatabaseContext db = new DatabaseContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Name == model.Name);
                }

                if (user == null)
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        db.Users.Add(new User { Name = model.Name, Email = model.Email, Password = model.Password });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Name == model.Name && u.Password == model.Password).FirstOrDefault();
                    }

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}