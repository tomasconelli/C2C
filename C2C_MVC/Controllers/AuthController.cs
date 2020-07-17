using C2C_MVC.Helpers;
using C2C_MVC.Models;
using C2C_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace C2C_MVC.Controllers
{

    public class AuthController : Controller
    {
        private readonly ApplicationDbContext db;
        public AuthController()
        {
            db = new ApplicationDbContext();
        }

        public bool Init()
        {
            if (Session["UserId"] == null)
                return false;
            return true;
        }
        // GET: Auth
        public ActionResult Index()
        {

            return View();
        }

        public void llenarCBRegistro()
        {
            lst = (from d in db.Cargoes
                   select new CargoViewModel
                   {
                       CargoId = d.CargoId,
                       CargoName = d.CargoName

                   }).ToList();
            List<SelectListItem> cargos = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.CargoName.ToString(),
                    Value = d.CargoId.ToString(),
                    Selected = false
                };
            });

            ViewBag.cargos = cargos;
        }
        List<CargoViewModel> lst = null;

        [HttpGet]
        public ActionResult Register()
        {
            
            if (Init() == false)
            {
                return RedirectToAction("Login", "Auth");
            }

            llenarCBRegistro();
            RegisterViewModel vm = new RegisterViewModel();
            return View(vm);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            llenarCBRegistro();
            if (ModelState.IsValid)
            {
                if (db.Users.Any(x => x.RutUser == model.RutUser))
                {
                    ViewData["ErrorMessage"] = "El Rut ya se encuentra registrado.";
                    return View();
                }
                var user = new User();
                user.RutUser = model.RutUser;
                user.NombreUser = model.NombreUser;
                user.ApellidoUser = model.ApellidoUser;
                user.SexoUser = model.SexoUser;
                user.TelefonoUser = model.TelefonoUser;
                user.DireccionUser = model.DireccionUser;
                byte[] psHash, psSalt;
                PasswordHelper.CreatePasswordHash(model.Password, out psHash, out psSalt);
                user.PasswordUserHash = psHash;
                user.PasswordUserSalt = psSalt;
                user.AliasUser = model.AliasUser;
                user.FenacUser = model.FenacUser;
                user.CargoId = model.CargoId;
                user.EmailUser = model.EmailUser;
                user.EstadoUser = model.EstadoUser;
                db.Users.Add(user);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Usuario Creado Correctamente";
                return RedirectToAction("Usuario", "Auth");
            }
            return View(model);
        }


        public ActionResult Login()
        {
            Session["RutUser"] = null;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.FirstOrDefault(x => x.RutUser == model.Rut);
               
                if (user != null && PasswordHelper.CheckPassword(model.Password, user.PasswordUserHash, user.PasswordUserSalt))
                {
                    Session["UserId"] = user.UserId;
                    InitOwin(user);
                    TempData["SuccessMessage"] = $"Bienvenido { user.NombreCompleto}";
                    return RedirectToAction("Index", "Home");
                }

                TempData["ErrorMessage"] = "Error al iniciar sesión";
                return RedirectToAction("Login");
            }
            TempData["ErrorMessage"] = "Error usuario y/o Contraseña";
            return RedirectToAction("Login");
        }




        private void InitOwin(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.RutUser),
                new Claim(ClaimTypes.Email, user.EmailUser),
                new Claim("FullName", user.NombreCompleto),
            };

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var cargos = db.Cargoes.FirstOrDefault(x => x.CargoId == user.CargoId);
            /* var cargos = await db.Cargoes.Where(x => x.CargoId == user.CargoId).Select(x => x.CargoName).ToListAsync();*/
            var cargo = cargos;
            identity.AddClaim(new Claim(ClaimTypes.Role, cargo.CargoName));

            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignIn(identity);
            
        }

        public ActionResult Usuario(string q)
        {
            if (Init() == false)
            {
                return RedirectToAction("Login", "Auth");
            }
            var users = db.Users.OrderBy(x => x.UserId).ToList();
            var cargos = db.Cargoes.OrderBy(z => z.CargoId).ToList();


            RegisterViewModel vm = new RegisterViewModel();
            vm.Users = users;
            vm.Cargos = cargos;

            if (!string.IsNullOrEmpty(q))
                vm.Users = vm.Users.Where(x => x.NombreCompleto.ToUpper().Contains(q.ToUpper())).ToList();

            foreach (var user in vm.Users)
            {
                user.Cargo = vm.Cargos.FirstOrDefault(x => x.CargoId == user.CargoId);
            }
            return View("Usuario", vm);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var users = db.Users.OrderBy(x => x.UserId).ToList();
            var user = users.FirstOrDefault(x => x.UserId == id);
            
            if (id == 0 || user == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }
            db.Users.Remove(user);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Uauario borrado correctamente";
            return RedirectToAction("Usuario", "Auth");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var users = db.Users.OrderBy(x => x.UserId).ToList();
            var user = users.FirstOrDefault(x => x.UserId == id);
            llenarCBRegistro();
            if (id == 0 || user == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");

            }

            RegisterViewModel vm = new RegisterViewModel();

            vm.UserId = user.UserId;
            vm.RutUser = user.RutUser;
            vm.NombreUser = user.NombreUser;
            vm.ApellidoUser = user.ApellidoUser;
            vm.SexoUser = user.SexoUser;
            vm.TelefonoUser = user.TelefonoUser;
            vm.DireccionUser = user.DireccionUser;
            vm.AliasUser = user.AliasUser;
            vm.FenacUser = user.FenacUser;
            vm.CargoId = user.CargoId;
            vm.EmailUser = user.EmailUser;
            vm.EstadoUser = user.EstadoUser;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(RegisterViewModel vm)
        {
            var users = db.Users.OrderBy(x => x.UserId).ToList();
            var user = users.FirstOrDefault(x => x.UserId == vm.UserId);

            if (user == null)
            {
                TempData["ErrorMessage"] = "El identificador no fue encontrado";
                return RedirectToAction("Index");
            }

            user.RutUser = vm.RutUser;
            user.NombreUser = vm.NombreUser;
            user.ApellidoUser = vm.ApellidoUser;
            user.SexoUser = vm.SexoUser;
            user.TelefonoUser = vm.TelefonoUser;
            user.DireccionUser = vm.DireccionUser;
            byte[] psHash, psSalt;
            PasswordHelper.CreatePasswordHash(vm.Password, out psHash, out psSalt);
            user.PasswordUserHash = psHash;
            user.PasswordUserSalt = psSalt;
            user.AliasUser = vm.AliasUser;
            user.FenacUser = vm.FenacUser;
            user.CargoId = vm.CargoId;
            user.EmailUser = vm.EmailUser;
            user.EstadoUser = vm.EstadoUser;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Usuario actualizado correctamente";
            return RedirectToAction("Usuario", "auth");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut(LoginViewModel model)
        {

            OutOwin();
            return RedirectToAction("Login");
        }

        private void OutOwin()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignOut();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}