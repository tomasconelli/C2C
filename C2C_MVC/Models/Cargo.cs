using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C2C_MVC.Models
{
    public class Cargo : BaseEntity
    {
        [Key]
        public int CargoId { get; set; }
        [Required]
        public string CargoName { get; set; }


    }
}