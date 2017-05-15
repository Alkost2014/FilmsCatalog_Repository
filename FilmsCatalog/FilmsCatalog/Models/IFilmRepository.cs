using FilmsCatalog.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsCatalog.Models
{
    public interface IFilmRepository<T> : IDisposable where T : class
    {
        User GetUser(string name);

        IEnumerable<T> GetFilmList();
        T GetFilm(int? id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
