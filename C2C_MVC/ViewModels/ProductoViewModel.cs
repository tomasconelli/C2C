using C2C_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C2C_MVC.ViewModels
{
    public class ProductoViewModel
    {
        public string q { get; set; }

        public List<Producto> Productos { get; set; }
        public List<Categoria> Categorias { get; set; }
        public int ProdutoId { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string NombreProducto { get; set; }
        [Required]
        [Range(1,2000)]
        [Display(Name = "Cantidad")]
        public int CantidadProducto { get; set; }
        [Required]
        [Range(1, 200000)]
        [Display(Name = "Precio de Compra")]
        public int PrecioCProducto { get; set; }
        [Required]
        [Range(1, 400000)]
        [Display(Name = "Precio de Venta")]
        public int PrecioVPrdoducto { get; set; }
        [Required]
        public int CategoriaId { get; set; }

    }
}