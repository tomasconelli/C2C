using C2C_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C2C_MVC.ViewModels
{
    public class DistribuidorViewModel
    {
        public string q { get; set; }

        public List<Distribuidor> Distribuidores { get; set; }

        public int DistribuidorId { get; set; }
        [Required]
        [Display(Name = "RUT")]
        public string RutDistribuidor { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string NombreDistribuidor { get; set; }
        [Required]
        [Display(Name = "Teléfono")]
        public string TelefonoDistribuidor { get; set; }
        [Required]
        [Display(Name = "Dirección")]
        public string DireccionDistribuidor { get; set; }
    }
}