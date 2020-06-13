﻿using C2C_MVC.Models;
using C2C_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C2C_MVC.Controllers
{
    public class CargoController : Controller
    {
        private readonly ApplicationDbContext db;
        public CargoController()
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
            var cargos = db.Cargoes.OrderBy(x => x.CargoId).ToList();

            CargoViewModel vm = new CargoViewModel();
            vm.Cargos = cargos;
            return View(vm);
        }
        [HttpGet]
        public ActionResult Crear()
        {
            if (init() == false)
            {
                return RedirectToAction("Login", "Auth");
            }
            var cargos = db.Cargoes.OrderBy(x => x.CargoId).ToList();

            CargoViewModel vm = new CargoViewModel();
            vm.Cargos = cargos;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(CargoViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Cargoes.Any(x => x.CargoName == model.CargoName))
                {
                    TempData["ErrorMessage"] = "El cargo ya se encuatra registrado";
                    return View("Index", model);
                }
                var cargo = new Cargo();
                cargo.CargoName = model.CargoName;
                db.Cargoes.Add(cargo);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Cargo Creada Correctamente";
                return RedirectToAction("Index", "Cargo");
            }

            var cargos = db.Cargoes.OrderBy(x => x.CargoId).ToList();
            
            model.Cargos = cargos;

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var cargos = db.Cargoes.OrderBy(x => x.CargoId).ToList();
            var cargo = cargos.FirstOrDefault(x => x.CargoId == id);
            if (id == 0 || cargo == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }
            db.Cargoes.Remove(cargo);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Cargo borrado correctamente";
            return RedirectToAction("Index", "Cargo");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var cargos = db.Cargoes.OrderBy(x => x.CargoId).ToList();
            var cargo = cargos.FirstOrDefault(x => x.CargoId == id);
            if (id == 0 || cargo == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }


            CargoViewModel vm = new CargoViewModel();

            vm.CargoId = cargo.CargoId;
            vm.CargoName = cargo.CargoName;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CargoViewModel vm)
        {
            var cargos = db.Cargoes.OrderBy(x => x.CargoId).ToList();
            var cargo = cargos.FirstOrDefault(x => x.CargoId == vm.CargoId);
            if (cargo == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");
            }

            cargo.CargoName = vm.CargoName;
            db.Entry(cargo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}