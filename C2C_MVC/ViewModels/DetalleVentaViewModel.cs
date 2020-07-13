using C2C_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace C2C_MVC.ViewModels
{
    public class DetalleVentaViewModel
    {
        public string q { get; set; }
        public List<Producto> Productos { get; set; }
        public List<Producto> Productos1 { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<MPago> MPagos { get; set; }
        public List<DetalleVenta> DetalleVentas { get; set; }
        public int DetalleVentaId { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        public int DetalleVentaCantidad { get; set; }
        [Required]
        public int DetalleVentaPrecio { get; set; }
        [Required]
        public int DetalleVentaTotal { get; set; }
        public string DetalleVentaObs { get; set; }
        [Required]
        public int VentaId { get; set; }
        [ForeignKey("ProdutoId")]
        public Producto Producto { get; set; }
        [ForeignKey("VentaId")]
        public Venta Venta { get; set; }
        
    }
}