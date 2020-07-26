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
    public class MPagoController : Controller
    {
        private readonly ApplicationDbContext db;
        public MPagoController()
        {
            db = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var mpagos = db.MPagoes.OrderBy(x => x.MPagoId).ToList();

            MPagoViewModel vm = new MPagoViewModel();
            vm.MPagos = mpagos;
            return View(vm);
        }
        [HttpGet]
        public ActionResult Crear()
        {
            var mpagos = db.MPagoes.OrderBy(x => x.MPagoId).ToList();

            MPagoViewModel vm = new MPagoViewModel();
            vm.MPagos = mpagos;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(MPagoViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.MPagoes.Any(x => x.MPagoName == model.MPagoName))
                {
                    TempData["ErrorMessage"] = "El medio de pago ya se encuatra registrado";
                    return View(model);
                }
                var mpago = new MPago();
                mpago.MPagoName = model.MPagoName;
                db.MPagoes.Add(mpago);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Medio de pago creado Correctamente";
                return RedirectToAction("Index", "MPago");
            }
            var mpagos = db.MPagoes.OrderBy(x => x.MPagoId).ToList();
            model.MPagos = mpagos;

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var mpagos = db.MPagoes.OrderBy(x => x.MPagoId).ToList();
            var mpago = mpagos.FirstOrDefault(x => x.MPagoId == id);
            if (id == 0 || mpago == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }
            db.MPagoes.Remove(mpago);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Medio de pago borrado correctamente";
            return RedirectToAction("Index", "MPago");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var mpagos = db.MPagoes.OrderBy(x => x.MPagoId).ToList();
            var mpago = mpagos.FirstOrDefault(x => x.MPagoId == id);
            if (id == 0 || mpago == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }


            MPagoViewModel vm = new MPagoViewModel();

            vm.MPagoId = mpago.MPagoId;
            vm.MPagoName = mpago.MPagoName;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(MPagoViewModel vm)
        {
            var mpagos = db.MPagoes.OrderBy(x => x.MPagoId).ToList();
            var mpago = mpagos.FirstOrDefault(x => x.MPagoId == vm.MPagoId);
            if (mpago == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");
            }

            mpago.MPagoName = vm.MPagoName;
            db.Entry(mpago).State = System.Data.Entity.EntityState.Modified;
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