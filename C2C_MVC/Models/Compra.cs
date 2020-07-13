using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace C2C_MVC.Models
{
    public class Compra
    {
        [Key]
        public int CompraId { get; set; }
        [Required]
        public int CompraMonto { get; set; }
        [Required]
        public int DistribuidorId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int MPagoId { get; set; }
        [Required]
        public string CompraFecha { get; set; }
        [Required]
        public string CompraHora { get; set; }
        [ForeignKey("DistribuidorId")]
        public Distribuidor Distribuidor { get; set; }
        [ForeignKey("MPagoId")]
        public MPago MPago { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}