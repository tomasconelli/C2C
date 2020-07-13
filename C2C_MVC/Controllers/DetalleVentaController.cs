using C2C_MVC.Models;
using C2C_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C2C_MVC.Controllers
{
    [Authorize]
    public class DetalleVentaController : Controller
    {


        private readonly ApplicationDbContext db;
        public DetalleVentaController()
        {

            db = new ApplicationDbContext();
        }

        public void llenarCBProductos()
        {
            lst1 = (from f in db.Productoes
                    select new ProductoViewModel
                    {
                        ProdutoId = f.ProdutoId,
                        NombreProducto = f.NombreProducto

                    }).ToList();
            List<SelectListItem> productos = lst1.ConvertAll(f =>
            {
                return new SelectListItem()
                {
                    Text = f.NombreProducto.ToString(),
                    Value = f.ProdutoId.ToString(),
                    Selected = false
                };
            });

            ViewBag.productos = productos;
        }
        List<ProductoViewModel> lst1 = null;

        public void llenarCBMpagos()
        {
            lst2 = (from f in db.MPagoes
                    select new MPagoViewModel
                    {
                        MPagoId = f.MPagoId,
                        MPagoName = f.MPagoName

                    }).ToList();
            List<SelectListItem> mpagos = lst2.ConvertAll(f =>
            {
                return new SelectListItem()
                {
                    Text = f.MPagoName.ToString(),
                    Value = f.MPagoId.ToString(),
                    Selected = false
                };
            });

            ViewBag.mpagos = mpagos;
        }
        List<MPagoViewModel> lst2 = null;

        private IQueryable<Producto> GetQuery(int? searchProductoId)
        {
            var query = db.Productoes.AsQueryable();

            if (searchProductoId != null)
                query = query.Where(x => x.ProdutoId == searchProductoId);

            return query;
            
        }

        private IQueryable<DetalleVenta> GetQuery1(int? searchTicketId)
        {
            var query = db.DetalleVentas.AsQueryable();

            if (searchTicketId != null)
                query = query.Where(x => x.VentaId == searchTicketId);

            return query;

        }

        public ActionResult GetProductoJson(int? searchProductoId)
        {
            IQueryable<Producto> query = GetQuery(searchProductoId);
            var productos = query.Include(x => x.Categoria).ToList();
            return Json(productos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductoJson1(int? searchTicketId)
        {
            IQueryable<DetalleVenta> query = GetQuery1(searchTicketId);
            var tickets = query.Include(x => x.VentaId).ToList();
            return Json(tickets, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(int? q, int? searchProductoId, int? searchTicketId)
        {
            DetalleVentaViewModel vm = new DetalleVentaViewModel();

            var lastId = db.Ventas.Select(x => x.VentaId).Max() + 1;
            ViewBag.lastId = lastId;

            
            var productos = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var productos1 = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var categorias = db.Categorias.OrderBy(z => z.CategoriaId).ToList();
            var detalleVentas = db.DetalleVentas.OrderBy(w => w.DetalleVentaId).ToList();
            vm.Productos = productos;
            vm.DetalleVentas = detalleVentas;
            vm.Categorias = categorias;

            

            var query = GetQuery(searchProductoId);
            var query1 = GetQuery1(searchTicketId);

            vm.Productos = query.OrderBy(x => x.NombreProducto).ToList();
            vm.DetalleVentas = query1.OrderBy(w => w.VentaId).ToList();


            vm.Productos1 = productos1;

            foreach (var producto in vm.Productos)
            {
                producto.Categoria = vm.Categorias.FirstOrDefault(x => x.CategoriaId == producto.CategoriaId);
            }

            foreach (var producto1 in vm.Productos)
            {
                producto1.Categoria = vm.Categorias.FirstOrDefault(x => x.CategoriaId == producto1.CategoriaId);
            }
            
            llenarCBProductos();
            llenarCBMpagos();
            if (q != 0)
                vm.DetalleVentas = vm.DetalleVentas.Where(y => y.VentaId == q).ToList();

            return View("index", vm);
        }

        /*[HttpPost]
        public ActionResult Seleccionar(string idProducto)
        {
            Producto objProducto = new Producto(idProducto);
            objProductoNeg.find(objProducto);
            return Json(objProducto, JsonRequestBehavior.AllowGet);

        }*/

        public ActionResult AgregarDetalle(DetalleVentaViewModel model)
        {
            DetalleVentaViewModel vm = new DetalleVentaViewModel();

            var lastId = db.Ventas.Select(x => x.VentaId).Max() + 1;
            ViewBag.lastId = lastId;


            var productos = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var productos1 = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var categorias = db.Categorias.OrderBy(z => z.CategoriaId).ToList();
            vm.Productos = productos;

            vm.Categorias = categorias;

            vm.Productos1 = productos1;

            foreach (var producto in vm.Productos)
            {
                producto.Categoria = vm.Categorias.FirstOrDefault(x => x.CategoriaId == producto.CategoriaId);
            }

            foreach (var producto1 in vm.Productos)
            {
                producto1.Categoria = vm.Categorias.FirstOrDefault(x => x.CategoriaId == producto1.CategoriaId);
            }

            llenarCBProductos();
            llenarCBMpagos();

            int q = model.DetalleVentaCantidad;
            int p = model.DetalleVentaPrecio;
            int t = q * p;
            int ticket = model.VentaId;

            

            if (ModelState.IsValid)
            {
                var detalleVenta = new DetalleVenta();
                detalleVenta.ProdutoId = model.ProdutoId;
                detalleVenta.DetalleVentaCantidad = model.DetalleVentaCantidad;
                detalleVenta.DetalleVentaPrecio = model.DetalleVentaPrecio;
                detalleVenta.DetalleVentaTotal = t;
                detalleVenta.VentaId = model.VentaId;
                detalleVenta.DetalleVentaObs = model.DetalleVentaObs;
                db.DetalleVentas.Add(detalleVenta);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Agregado";
                return RedirectToAction("Index");
            }



            return View("Index", vm);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var detalleVentas = db.DetalleVentas.OrderBy(x => x.DetalleVentaId).ToList();
            var detalleVenta = detalleVentas.FirstOrDefault(x => x.DetalleVentaId == id);
            if (id == 0 || detalleVenta == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }
            db.DetalleVentas.Remove(detalleVenta);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Item borrado correctamente";
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}