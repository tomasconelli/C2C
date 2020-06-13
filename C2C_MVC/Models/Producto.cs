using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace C2C_MVC.Models
{
    public class Producto
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required]
        public string NombreProducto { get; set; }
        [Required]
        public int CantidadProducto { get; set; }
        [Required]
        public int PrecioCProducto { get; set; }
        [Required]
        public int PrecioVPrdoducto { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
    }
}