namespace C2C_MVC.Migrations
{
    using C2C_MVC.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<C2C_MVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(C2C_MVC.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Cargoes.Any())
            {
                context.Cargoes.Add(new Cargo
                {
                    CargoName = "Administrador",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
                    );
                context.Cargoes.Add(new Cargo
                {
                    CargoName = "Vendedor",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
                    );
            }
        }
    }
}
