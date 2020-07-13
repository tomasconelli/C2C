using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace C2C_MVC.Models
{
    public class DetalleCompra
    {
        [Key]
        public int DetalleCompraId { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        public int DetalleCompraCantidad { get; set; }
        [Required]
        public int DetalleCompraPrecio { get; set; }
        [Required]
        public int DetalleCompraTotal { get; set; }
        [Required]
        public int CompraId { get; set; }
        [ForeignKey("ProdutoId")]
        public Producto Producto { get; set; }
        [ForeignKey("CompraId")]
        public Compra Compra { get; set; }
    }
}