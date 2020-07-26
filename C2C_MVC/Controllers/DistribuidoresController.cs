using C2C_MVC.Models;
using C2C_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C2C_MVC.Controllers
{
    [Authorize]
    public class DistribuidoresController : Controller
    {
        private readonly ApplicationDbContext db;
        public DistribuidoresController()
        {
            db = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var distribuides = db.Distribuidors.OrderBy(x => x.NombreDistribuidor).ToList();

            DistribuidorViewModel vm = new DistribuidorViewModel();
            vm.Distribuidores = distribuides;
            return View(vm);
        }
        [HttpGet]
        public ActionResult Crear()
        {
            var distribuides = db.Distribuidors.OrderBy(x => x.NombreDistribuidor).ToList();

            DistribuidorViewModel vm = new DistribuidorViewModel();
            vm.Distribuidores = distribuides;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(DistribuidorViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Distribuidors.Any(x => x.RutDistribuidor == model.RutDistribuidor))
                {
                    TempData["ErrorMessage"] = "El distribuidor ya se encuatra registrado";
                    return View("Index", model);
                }
                var distribuidor = new Distribuidor();
                distribuidor.RutDistribuidor = model.RutDistribuidor;
                distribuidor.NombreDistribuidor = model.NombreDistribuidor;
                distribuidor.TelefonoDistribuidor = model.TelefonoDistribuidor;
                distribuidor.DireccionDistribuidor = model.DireccionDistribuidor;
                db.Distribuidors.Add(distribuidor);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Distribuidor Creado Correctamente";
                return RedirectToAction("Index", "Distribuidores");
            }

            var distribuides = db.Distribuidors.OrderBy(x => x.NombreDistribuidor).ToList();
            model.Distribuidores = distribuides;
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var distribuides = db.Distribuidors.OrderBy(x => x.NombreDistribuidor).ToList();
            var distribuidor = distribuides.FirstOrDefault(x => x.DistribuidorId == id);
            if (id == 0 || distribuidor == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }
            db.Distribuidors.Remove(distribuidor);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Distribuidor borrado correctamente";
            return RedirectToAction("Index", "Distribuidores");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var distribuides = db.Distribuidors.OrderBy(x => x.NombreDistribuidor).ToList();
            var distribuidor = distribuides.FirstOrDefault(x => x.DistribuidorId == id);
            if (id == 0 || distribuidor == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }


            DistribuidorViewModel vm = new DistribuidorViewModel();

            vm.DistribuidorId = distribuidor.DistribuidorId;
            vm.RutDistribuidor = distribuidor.RutDistribuidor;
            vm.NombreDistribuidor = distribuidor.NombreDistribuidor;
            vm.TelefonoDistribuidor = distribuidor.TelefonoDistribuidor;
            vm.DireccionDistribuidor = distribuidor.DireccionDistribuidor;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(DistribuidorViewModel vm)
        {
            var distribuides = db.Distribuidors.OrderBy(x => x.NombreDistribuidor).ToList();
            var distribuidor = distribuides.FirstOrDefault(x => x.DistribuidorId == vm.DistribuidorId);



            if (distribuidor == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");
            }

            distribuidor.RutDistribuidor = vm.RutDistribuidor;
            distribuidor.NombreDistribuidor = vm.NombreDistribuidor;
            distribuidor.TelefonoDistribuidor = vm.TelefonoDistribuidor;
            distribuidor.DireccionDistribuidor = vm.DireccionDistribuidor;
            db.Entry(distribuidor).State = System.Data.Entity.EntityState.Modified;
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