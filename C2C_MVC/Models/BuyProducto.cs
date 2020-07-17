using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace C2C_MVC.Models
{
    public class BuyProducto
    {
        [Key]
        public int BuyProductoId { get; set; }
        [Required]
        public int BuyId { get; set; }
        public int ProdutoId { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public int Total => Precio * Cantidad;
        [ForeignKey("BuyId")]
        public Buy Buy { get; set; }
        [ForeignKey("ProdutoId")]
        public Producto Producto { get; set; }
    }
}