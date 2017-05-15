using FilmsCatalog.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FilmsCatalog.Models
{
    public class FilmRepository : IFilmRepository<Film>
    {
        private DatabaseContext _db;
        private static IFilmRepository<Film> _repository;

        private FilmRepository()
        {
            _db = new DatabaseContext();
        }

        public static IFilmRepository<Film> CreateInstance()
        {
            if (_repository == null)
                _repository = new FilmRepository();

            return _repository;
        }


        public User GetUser(string name)
        {
            return _db.Users.Where(u => u.Name == name).FirstOrDefault();
        }


        public IEnumerable<Film> GetFilmList()
        {
            return _db.Films;
        }

        public Film GetFilm(int? id)
        {
            return _db.Films.Find(id);
        }

        public void Create(Film film)
        {
            _db.Films.Add(film);
            _db.SaveChanges();
        }

        public void Update(Film film)
        {
            _db.Entry(film).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(Film film)
        {
            _db.Films.Remove(film);
            _db.SaveChanges();
        }


        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}