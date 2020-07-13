using C2C_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace C2C_MVC.ViewModels
{
    public class VentaViewModel
    {
        public List<Venta> Ventas { get; set; }
        public string q { get; set; }
        public List<Producto> Productos { get; set; }
        public List<Categoria> Categorias { get; set; }
        public int VentaId { get; set; }
        [Required]
        public int VentaMonto { get; set; }
        [Required]
        [Display(Name = "Nombre Cliente")]
        public string NombreCVenta { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int MPagoId { get; set; }
        [Required]
        public string VentaFecha { get; set; }
        [Required]
        public string VentaHora { get; set; }
        [ForeignKey("MPagoId")]
        public MPago MPago { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}