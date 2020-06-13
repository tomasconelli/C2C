using C2C_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C2C_MVC.ViewModels
{
    public class CategoriaViewModel
    {
        public string q { get; set; }

        public List<Categoria> Categorias { get; set; }
        public int CategoriaId { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string CategoriaName { get; set; }
    }
}