using C2C_MVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

using C2C_MVC.ViewModels;

namespace C2C_MVC.Controllers
{
    [Authorize]
    public class BuyController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Buy
        public ActionResult Index()
        {
            var buys = db.Buys
                .Include(x => x.Author)
                .Include(x => x.BuyProductos)
                        .OrderByDescending(x => x.BuyId)
                        .ToList();
            return View(buys);
        }

        public ActionResult Create()
        {
            var user = User.Identity.GetUserId<int>();
            Buy buy = new Buy
            {
                CreatedAt = DateTime.Now,
                AuthorId = User.Identity.GetUserId<int>()
            };

            db.Buys.Add(buy);
            db.SaveChanges();
            return RedirectToAction("index", new { id = buy.BuyId });
        }

        public ActionResult Edit(int id)
        {
            var buy = db.Buys.Include(x => x.BuyProductos)
                .FirstOrDefault(x => x.BuyId == id);
            var vm = new BuyViewModel();
            vm.Buy = buy;
            vm.Productos = db.Productoes.OrderBy(x => x.NombreProducto).ToList();
            return View(vm);
        }

        public ActionResult AddProduct(BuyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var producto = db.Productoes.Find(model.ProductoId);
                if(producto == null)
                {
                    TempData["ErrorMessage"] = "Producto no encontrado";
                    return RedirectToAction("Edit", new { id = model.BuyId });
                }
                var buyProducto = new BuyProducto
                {
                    ProdutoId = model.ProductoId,
                    Precio = producto.PrecioCProducto,
                    BuyId = model.BuyId,
                    Cantidad = model.Quantity
                };

                var SProdutoId = model.ProductoId;
                
                var productos = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
                var sproducto = productos.FirstOrDefault(x => x.ProdutoId == SProdutoId);
                var stock = sproducto.CantidadProducto;
                var newStock = model.Quantity + stock;

                sproducto.CantidadProducto = newStock;

                db.BuyProductos.Add(buyProducto);
                db.Entry(sproducto).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Agregado correctamente";
                return RedirectToAction("Edit", new { id = model.BuyId });
            }
            TempData["ErrorMessage"] = "Faltan Datos";
            return RedirectToAction("Edit", new { id = model.BuyId });
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}