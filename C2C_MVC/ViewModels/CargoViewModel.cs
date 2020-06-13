using C2C_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C2C_MVC.ViewModels
{
    public class CargoViewModel
    {
        public string q { get; set; }

        public List<Cargo> Cargos { get; set; }
        public int CargoId { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string CargoName { get; set; }


    }
}