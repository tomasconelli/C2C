using C2C_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C2C_MVC.ViewModels
{
    public class MPagoViewModel
    {
        public string q { get; set; }

        public List<MPago> MPagos { get; set; }
        public int MPagoId { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string MPagoName { get; set; }
    }
}