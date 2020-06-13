namespace C2C_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TodoDeNuevo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cargoes",
                c => new
                    {
                        CargoId = c.Int(nullable: false, identity: true),
                        CargoName = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.CargoId);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        CategoriaName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        NombreProducto = c.String(nullable: false),
                        CantidadProducto = c.Int(nullable: false),
                        PrecioCProducto = c.Int(nullable: false),
                        PrecioVPrdoducto = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProdutoId)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        RutUser = c.String(nullable: false),
                        NombreUser = c.String(nullable: false),
                        ApellidoUser = c.String(nullable: false),
                        TelefonoUser = c.String(nullable: false),
                        DireccionUser = c.String(nullable: false),
                        PasswordUserHash = c.Binary(nullable: false),
                        PasswordUserSalt = c.Binary(),
                        AliasUser = c.String(nullable: false),
                        FenacUser = c.String(nullable: false),
                        CargoId = c.Int(nullable: false),
                        EmailUser = c.String(nullable: false),
                        EstadoUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Cargoes", t => t.CargoId, cascadeDelete: true)
                .Index(t => t.CargoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "CargoId", "dbo.Cargoes");
            DropForeignKey("dbo.Productoes", "CategoriaId", "dbo.Categorias");
            DropIndex("dbo.Users", new[] { "CargoId" });
            DropIndex("dbo.Productoes", new[] { "CategoriaId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Productoes");
            DropTable("dbo.Categorias");
            DropTable("dbo.Cargoes");
        }
    }
}
