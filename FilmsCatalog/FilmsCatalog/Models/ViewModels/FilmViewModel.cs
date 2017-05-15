using FilmsCatalog.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmsCatalog.Models.ViewModels
{
    public class FilmViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Caption { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Год выпуска")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Режиссер")]
        public string Producer { get; set; }

        [Required]
        [Display(Name = "Постер")]
        public string Poster { get; set; }
    }
}