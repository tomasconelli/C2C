using C2C_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C2C_MVC.ViewModels
{
    public class RegisterViewModel
    {
        public string q { get; set; }

        public List<User> Users { get; set; }
        public List<Cargo> Cargos { get; set; }
        public int UserId { get; set; }
        [Required]
        [Display(Name = "RUT")]
        public string RutUser { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string NombreUser { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        public string ApellidoUser { get; set; }
        [Required]
        [Display(Name = "Teléfono")]
        public string TelefonoUser { get; set; }
        [Required]
        [Display(Name = "Dirección")]
        public string DireccionUser { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Alias")]
        public string AliasUser { get; set; }
        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public string FenacUser { get; set; }
        [Required]
        [Display(Name = "Cargo")]
        public int CargoId { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailUser { get; set; }
        [Required]
        public string EstadoUser { get; set; }
    }
}