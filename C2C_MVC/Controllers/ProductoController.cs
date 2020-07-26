using C2C_MVC.Models;
using C2C_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C2C_MVC.Controllers
{
    [Authorize(Roles =Helpers.StringHelper.ROLE_ADMINISTRATOR)]
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext db;
        public ProductoController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Auth
        public ActionResult Index(string q)
        {

            var productos = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var categorias = db.Categorias.OrderBy(z => z.CategoriaId).ToList();
            

            ProductoViewModel vm = new ProductoViewModel();
            llenarCBProductos();
            vm.Productos = productos;
            vm.Categorias = categorias;

            foreach (var producto in vm.Productos)
            {
                producto.Categoria = vm.Categorias.FirstOrDefault(x => x.CategoriaId == producto.CategoriaId);
            }
            return View("Index", vm);
        }

        public void llenarCBProductos()
        {
            lst1 = (from f in db.Categorias
                    select new CategoriaViewModel
                    {
                        CategoriaId = f.CategoriaId,
                        CategoriaName = f.CategoriaName

                    }).ToList();
            List<SelectListItem> categorias = lst1.ConvertAll(f =>
            {
                return new SelectListItem()
                {
                    Text = f.CategoriaName.ToString(),
                    Value = f.CategoriaId.ToString(),
                    Selected = false
                };
            });

            ViewBag.categorias = categorias;
        }
        List<CategoriaViewModel> lst1 = null;

        [HttpGet]
        public ActionResult Crear()
        {

            ProductoViewModel vm = new ProductoViewModel();
            llenarCBProductos();

            return View("Index", vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(ProductoViewModel model)
        {
            llenarCBProductos();
            if (ModelState.IsValid)
            {
                if (db.Productoes.Any(x => x.NombreProducto == model.NombreProducto))
                {
                    ViewData["ErrorMessage"] = "El Producto ya se encuentra registrado.";
                    return View(model);
                }
                var producto = new Producto();
                producto.NombreProducto = model.NombreProducto;
                producto.CantidadProducto = model.CantidadProducto;
                producto.PrecioCProducto = model.PrecioCProducto;
                producto.PrecioVPrdoducto = model.PrecioVPrdoducto;
                producto.CategoriaId = model.CategoriaId;
                db.Productoes.Add(producto);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Producto Creado Correctamente";
                return RedirectToAction("Index", "Producto");
            }

            var productos = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var categorias = db.Categorias.OrderBy(z => z.CategoriaId).ToList();



            model.Productos = productos;
            model.Categorias = categorias;

            return View("Index",model);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var productos = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var producto = productos.FirstOrDefault(x => x.ProdutoId == id);
            if (id == 0 || producto == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }
            db.Productoes.Remove(producto);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Producto borrado correctamente";
            return RedirectToAction("Index", "Producto");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var productos = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var producto = productos.FirstOrDefault(x => x.ProdutoId == id);
            llenarCBProductos();
            if (id == 0 || producto == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }

            var categorias = db.Categorias.OrderBy(z => z.CategoriaId).ToList();


            ProductoViewModel vm = new ProductoViewModel();

            vm.ProdutoId = producto.ProdutoId;
            vm.NombreProducto = producto.NombreProducto;
            vm.CantidadProducto = producto.CantidadProducto;
            vm.PrecioCProducto = producto.PrecioCProducto;
            vm.PrecioVPrdoducto = producto.PrecioVPrdoducto;
            vm.CategoriaId = producto.CategoriaId;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProductoViewModel vm)
        {
            var categorias = db.Categorias.OrderBy(z => z.CategoriaId).ToList();
            var productos = db.Productoes.OrderBy(x => x.ProdutoId).ToList();
            var producto = productos.FirstOrDefault(x => x.ProdutoId == vm.ProdutoId);
            


            if (producto == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");
            }

            producto.NombreProducto = vm.NombreProducto;
            producto.CantidadProducto = vm.CantidadProducto;
            producto.PrecioCProducto = vm.PrecioCProducto;
            producto.PrecioVPrdoducto = vm.PrecioVPrdoducto;
            producto.CategoriaId = vm.CategoriaId;
            db.Entry(producto).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

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