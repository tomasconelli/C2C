using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C2C_MVC.Models
{
    public class Distribuidor
    {
        [Key]
        public int DistribuidorId { get; set; }
        [Required]
        public string RutDistribuidor { get; set; }
        [Required]
        public string NombreDistribuidor { get; set; }
        [Required]
        public string TelefonoDistribuidor { get; set; }
        [Required]
        public string DireccionDistribuidor { get; set; }
    }
}