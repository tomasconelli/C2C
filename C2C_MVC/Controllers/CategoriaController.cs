using C2C_MVC.Models;
using C2C_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C2C_MVC.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext db;
        public CategoriaController()
        {
            db = new ApplicationDbContext();
        }

        public bool init()
        {
            if (Session["UserId"] == null)
                return false;
            return true;
        }
        [HttpGet]
        public ActionResult Index()
        {
            if (init() == false)
            {
                return RedirectToAction("Login", "Auth");
            }
            var categorias = db.Categorias.OrderBy(x => x.CategoriaId).ToList();

            CategoriaViewModel vm = new CategoriaViewModel();
            vm.Categorias = categorias;
            return View(vm);
        }
        [HttpGet]
        public ActionResult Crear()
        {
            if (init() == false)
            {
                return RedirectToAction("Login", "Auth");
            }
            var categorias = db.Categorias.OrderBy(x => x.CategoriaId).ToList();

            CategoriaViewModel vm = new CategoriaViewModel();
            vm.Categorias = categorias;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(CategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Categorias.Any(x => x.CategoriaName == model.CategoriaName))
                {
                    TempData["ErrorMessage"] = "La Categoría ya se encuatra registrada";
                    return View("Index", model);
                }
                var categoria = new Categoria();
                categoria.CategoriaName = model.CategoriaName;
                db.Categorias.Add(categoria);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Categoria Creada Correctamente";
                return RedirectToAction("Index", "Categoria");
            }
            var categorias = db.Categorias.OrderBy(x => x.CategoriaId).ToList();

            model.Categorias = categorias;


            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var categorias = db.Categorias.OrderBy(x => x.CategoriaId).ToList();
            var categoria = categorias.FirstOrDefault(x => x.CategoriaId == id);
            if (id == 0 || categoria == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }
            db.Categorias.Remove(categoria);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Categoria borrada correctamente";
            return RedirectToAction("Index", "Categoria");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var categorias = db.Categorias.OrderBy(x => x.CategoriaId).ToList();
            var categoria = categorias.FirstOrDefault(x => x.CategoriaId == id);
            if (id == 0 || categoria == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }


            CategoriaViewModel vm = new CategoriaViewModel();

            vm.CategoriaId = categoria.CategoriaId;
            vm.CategoriaName = categoria.CategoriaName;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CategoriaViewModel vm)
        {
            var categorias = db.Categorias.OrderBy(x => x.CategoriaId).ToList();
            var categoria = categorias.FirstOrDefault(x => x.CategoriaId == vm.CategoriaId);
            if (categoria == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");
            }

            categoria.CategoriaName = vm.CategoriaName;
            db.Entry(categoria).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Categoria actualizada correctamente";

            return RedirectToAction("Index");
        }

    }
}