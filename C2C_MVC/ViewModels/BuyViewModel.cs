using C2C_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C2C_MVC.ViewModels
{
    public class BuyViewModel
    {
        [Required]
        public int BuyId { get; set; }
        [Required]
        public int ProductoId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Buy Buy { get; set; }
        public List<Producto> Productos { get; set; }
    }
}