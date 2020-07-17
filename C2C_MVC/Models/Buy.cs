using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace C2C_MVC.Models
{
    public class Buy : BaseEntity
    {
        public int BuyId { get; set; }
        [Required]
        public int AuthorId { get; set; }

        public int Total => BuyProductos?.Sum(x => x.Total) ?? 0;

        public List<BuyProducto> BuyProductos { get; set; }
        [ForeignKey("AuthorId")]
        public User Author { get; set; }
    }
}