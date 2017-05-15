using FilmsCatalog.Common;
using FilmsCatalog.Models;
using FilmsCatalog.Models.DomainModels;
using FilmsCatalog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmsCatalog.Controllers
{
    public class HomeController : Controller
    {
        private IFilmRepository<Film> _repository;

        public HomeController()
        {
            _repository = FilmRepository.CreateInstance();
        }

        //[Authorize]
        //public ActionResult Index()
        //{
        //    IEnumerable<Film> films = _repository.GetFilmList();

        //    User currentUser = _repository.GetUser(User.Identity.Name);
        //    ViewBag.CurrentUserId = currentUser.Id;

        //    return View(films);
        //}

        // С использованием пагинации
        [Authorize]
        public ActionResult Index(int page = 1)
        {
            int pageSize = 3; // количество объектов на страницу

            IEnumerable<Film> films = _repository.GetFilmList();
            IEnumerable<Film> filmsPerPage = films.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = films.Count() };
            IndexViewModel pageFilms = new IndexViewModel { Films = filmsPerPage, PageInfo = pageInfo };

            User currentUser = _repository.GetUser(User.Identity.Name);
            ViewBag.CurrentUserId = currentUser.Id;

            return View(pageFilms);
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Film film = _repository.GetFilm(id);
            if (film == null)
            {
                return HttpNotFound();
            }

            FilmViewModel model = new FilmViewModel();
            model.Id = film.Id;
            model.Caption = film.Caption;
            model.Description = film.Description;
            model.Year = film.Year;
            model.Producer = film.Producer;
            model.Poster = film.Poster;

            return View(model);
        }


        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FilmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User currentUser = _repository.GetUser(User.Identity.Name);

            Film film = new Film();
            film.Caption = model.Caption;
            film.Description = model.Description;
            film.Year = model.Year;
            film.Producer = model.Producer;
            //film.Poster = model.Poster;
            film.User = currentUser;

            try
            {
                HttpPostedFileBase poster = Request.Files["Poster"];
                if (!poster.ContentType.Contains("image"))
                {
                    return View(model);
                }
                string newFileName = HashHelper.GetMd5Hash(DateTime.Now.Ticks.ToString());
                string newFullFileName = newFileName + Path.GetExtension(poster.FileName);
                string filePath = Path.Combine(Server.MapPath("~/UploadFiles"), newFullFileName);
                poster.SaveAs(filePath);
                film.Poster = newFullFileName;
            }
            catch (Exception ex)
            {
                return View(model);
            }

            _repository.Create(film);

            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Film film = _repository.GetFilm(id);
            if (film == null)
            {
                return HttpNotFound();
            }

            FilmViewModel model = new FilmViewModel();
            model.Id = film.Id;
            model.Caption = film.Caption;
            model.Description = film.Description;
            model.Year = film.Year;
            model.Producer = film.Producer;
            model.Poster = film.Poster;

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(FilmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User currentUser = _repository.GetUser(User.Identity.Name);

            Film film = _repository.GetFilm(model.Id);
            film.Caption = model.Caption;
            film.Description = model.Description;
            film.Year = model.Year;
            film.Producer = model.Producer;
            //film.Poster = model.Poster;
            film.User = currentUser;

            try
            {
                if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/UploadFiles"), film.Poster)))
                {
                    System.IO.File.Delete(Path.Combine(Server.MapPath("~/UploadFiles"), film.Poster));
                }

                HttpPostedFileBase poster = Request.Files["Poster"];
                if (!poster.ContentType.Contains("image"))
                {
                    return View(model);
                }
                string newFileName = HashHelper.GetMd5Hash(DateTime.Now.Ticks.ToString());
                string newFullFileName = newFileName + Path.GetExtension(poster.FileName);
                string filePath = Path.Combine(Server.MapPath("~/UploadFiles"), newFullFileName);
                poster.SaveAs(filePath);
                film.Poster = newFullFileName;
            }
            catch (Exception ex)
            {
                return View(model);
            }

            _repository.Update(film);

            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Film film = _repository.GetFilm(id);
            if (film == null)
            {
                return HttpNotFound();
            }

            _repository.Delete(film);

            if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/UploadFiles"), film.Poster)))
            {
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/UploadFiles"), film.Poster));
            }

            return RedirectToAction("Index");
        }
    }
}