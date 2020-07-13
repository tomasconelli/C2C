using C2C_MVC.Helpers;
using C2C_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2C_MVC.Migrations
{
    public class UserSeeder
    {

        public static void Seeder(ApplicationDbContext context)
        {
            if (context.Users.Any(x => x.RutUser == "1-9") == false)
            {
                byte[] psHash, psSalt;
                PasswordHelper.CreatePasswordHash("password", out psHash, out psSalt);
                context.Users.Add(new User
                {
                    RutUser = "1-9",
                    NombreUser = "Administrador",
                    ApellidoUser = "General",
                    TelefonoUser = "996091142",
                    DireccionUser = "Lircay # 138",
                    PasswordUserHash = psHash,
                    PasswordUserSalt = psSalt,
                    AliasUser = "AG",
                    FenacUser = "2000-01-01",
                    CargoId = 1,
                    EmailUser = "ag@c2c.cl",
                    EstadoUser = "Activo"

                });
            }
        }

    }
}