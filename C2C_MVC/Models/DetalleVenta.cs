using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace C2C_MVC.Models
{
    public class DetalleVenta
    {
        [Key]
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


    }
}