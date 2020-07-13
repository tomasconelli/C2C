namespace C2C_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableVenta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetalleVentas",
                c => new
                    {
                        DetalleVentaId = c.Int(nullable: false, identity: true),
                        ProdutoId = c.Int(nullable: false),
                        DetalleVentaCantidad = c.Int(nullable: false),
                        DetalleVentaPrecio = c.Int(nullable: false),
                        DetalleVentaTotal = c.Int(nullable: false),
                        VentaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DetalleVentaId)
                .ForeignKey("dbo.Productoes", t => t.ProdutoId, cascadeDelete: true)
                .ForeignKey("dbo.Ventas", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.ProdutoId)
                .Index(t => t.VentaId);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        VentaId = c.Int(nullable: false, identity: true),
                        VentaMonto = c.Int(nullable: false),
                        NombreCVenta = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        MPagoId = c.Int(nullable: false),
                        VentaFecha = c.String(nullable: false),
                        VentaHora = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.VentaId)
                .ForeignKey("dbo.MPagoes", t => t.MPagoId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MPagoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleVentas", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.Ventas", "UserId", "dbo.Users");
            DropForeignKey("dbo.Ventas", "MPagoId", "dbo.MPagoes");
            DropForeignKey("dbo.DetalleVentas", "ProdutoId", "dbo.Productoes");
            DropIndex("dbo.Ventas", new[] { "MPagoId" });
            DropIndex("dbo.Ventas", new[] { "UserId" });
            DropIndex("dbo.DetalleVentas", new[] { "VentaId" });
            DropIndex("dbo.DetalleVentas", new[] { "ProdutoId" });
            DropTable("dbo.Ventas");
            DropTable("dbo.DetalleVentas");
        }
    }
}
