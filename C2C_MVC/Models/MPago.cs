using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C2C_MVC.Models
{
    public class MPago
    {
        [Key]
        public int MPagoId { get; set; }
        [Required]
        public string MPagoName { get; set; }
    }
}