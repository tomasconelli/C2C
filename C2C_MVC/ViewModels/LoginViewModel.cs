using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C2C_MVC.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Rut { get; set; }
        [Required]
        public string Password { get; set; }
    }
}