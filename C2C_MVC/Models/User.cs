using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace C2C_MVC.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string RutUser { get; set; }
        [Required]
        public string NombreUser { get; set; }
        [Required]
        public string ApellidoUser { get; set; }
        [Required]
        public string TelefonoUser { get; set; }
        [Required]
        public string DireccionUser { get; set; }
        [Required]
        public byte[] PasswordUserHash { get; set; }
        public byte[] PasswordUserSalt { get; set; }
        [Required]
        public string AliasUser { get; set; }
        [Required]
        public string FenacUser { get; set; }
        [Required]
        public int CargoId { get; set; }
        [Required]
        [EmailAddress]
        public string EmailUser { get; set; }
        [Required]
        public string EstadoUser { get; set; }
        [ForeignKey("CargoId")]
        public Cargo Cargo { get; set; }
        public string NombreCompleto => $"{NombreUser} {ApellidoUser}";
    }
}