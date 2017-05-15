using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmsCatalog.Models.DomainModels
{
    public class Film
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Caption { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Год выпуска")]
        public int Year { get; set; }
        [Display(Name = "Режиссер")]
        public string Producer { get; set; }
        [Display(Name = "Постер")]
        public string Poster { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}