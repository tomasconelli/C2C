using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C2C_MVC.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        [Required]
        public string CategoriaName { get; set; }
    }
}