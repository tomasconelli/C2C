using C2C_MVC.Models;
using C2C_MVC.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C2C_MVC.Controllers
{
    [Authorize]
    public class VentaController : Controller
    {
        

        private readonly ApplicationDbContext db;
        public VentaController()
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

        public ActionResult Index(int? searchProductoId)
        {
            DetalleVentaViewModel vm = new DetalleVentaViewModel();
            VentaViewModel vm1 = new VentaViewModel();

            var lastId = db.Ventas.Select(x => x.VentaId).Max() + 1;
            ViewBag.lastId = lastId;

            var query = db.Productoes.AsQueryable();
            var ventas = db.Ventas.OrderBy(r => r.VentaId).ToList();
            var productos = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var productos1 = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var categorias = db.Categorias.OrderBy(z => z.CategoriaId).ToList();
            vm1.Ventas = ventas;
            vm.Productos = productos;
            
            vm.Categorias = categorias;

            if (searchProductoId != null)
                query = query.Where(x => x.ProdutoId == searchProductoId);
                vm.Productos = query.OrderBy(x => x.NombreProducto).ToList();

            
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
            return View("Index", vm);
        }

        public ActionResult Venta(int? searchProductoId, int? MPagoId)
        {
            DetalleVentaViewModel vm = new DetalleVentaViewModel();
            VentaViewModel vm1 = new VentaViewModel();

            var lastId = db.Ventas.Select(x => x.VentaId).Max() + 1;
            ViewBag.lastId = lastId;

            var query = db.Productoes.AsQueryable();
            var query1 = db.Ventas.AsQueryable();
            var ventas = db.Ventas.OrderByDescending(r => r.VentaId).ToList();
            var productos = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var productos1 = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var categorias = db.Categorias.OrderBy(z => z.CategoriaId).ToList();
            var users = db.Users.OrderBy(s => s.UserId).ToList();
            var mpagos = db.MPagoes.OrderBy(t => t.MPagoId).ToList();
            vm1.Ventas = ventas;
            vm.Productos = productos;

            vm.Categorias = categorias;

            if (searchProductoId != null)
                query = query.Where(x => x.ProdutoId == searchProductoId);
            vm.Productos = query.OrderBy(x => x.NombreProducto).ToList();

            if (MPagoId != null)
                query1 = query1.Where(x => x.MPagoId == MPagoId);
            vm1.Ventas = query1.OrderByDescending(x => x.VentaId).ToList();

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
            return View("Venta", vm1);
        }

        /*[HttpPost]
        public ActionResult Seleccionar(string idProducto)
        {
            Producto objProducto = new Producto(idProducto);
            objProductoNeg.find(objProducto);
            return Json(objProducto, JsonRequestBehavior.AllowGet);

        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerarVenta(VentaViewModel model)
        {
            llenarCBProductos();
            var user = User.Identity.GetUserId<int>();
            if (ModelState.IsValid)
            {
                var lastId = db.Ventas.Select(x => x.VentaId).Max() + 1;
                var total = db.DetalleVentas.Where(x => x.VentaId == lastId).ToList();
                var total1 = total.Sum(x => x.DetalleVentaTotal);


                var venta = new Venta();
                venta.VentaMonto = total1;
                venta.NombreCVenta = model.NombreCVenta;
                venta.UserId = user;
                venta.MPagoId = model.MPagoId;
                venta.VentaFecha = model.VentaFecha;
                venta.VentaHora = model.VentaHora;
                db.Ventas.Add(venta);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Producto Creado Correctamente";
                return RedirectToAction("Index","DetalleVenta");
            }

            var productos = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var categorias = db.Categorias.OrderBy(z => z.CategoriaId).ToList();



            model.Productos = productos;
            model.Categorias = categorias;

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Detalles(int id)
        {
            var productos = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var ventas = db.Ventas.OrderBy(r => r.VentaId).ToList();
            var venta = ventas.FirstOrDefault(x => x.VentaId == id);
            var detalleVentas = db.DetalleVentas.OrderBy(t => t.VentaId).ToList();
            var detalleVenta = detalleVentas.FirstOrDefault(v => v.VentaId == id);
            llenarCBProductos();
            if (id == 0 || venta == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }

            var categorias = db.Categorias.OrderBy(z => z.CategoriaId).ToList();


            DetalleVentaViewModel vm = new DetalleVentaViewModel();

            vm.VentaId = detalleVenta.VentaId;


            return View(vm);
        }

        public ActionResult Detalle(int id)
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
            if (id != 0)
                vm.DetalleVentas = vm.DetalleVentas.Where(y => y.VentaId == id).ToList();
            return View("detalle", vm);
        }


    }
}