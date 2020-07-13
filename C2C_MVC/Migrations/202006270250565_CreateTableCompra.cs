namespace C2C_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCompra : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        CompraId = c.Int(nullable: false, identity: true),
                        CompraMonto = c.Int(nullable: false),
                        DistribuidorId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        MPagoId = c.Int(nullable: false),
                        CompraFecha = c.String(nullable: false),
                        CompraHora = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CompraId)
                .ForeignKey("dbo.Distribuidors", t => t.DistribuidorId, cascadeDelete: true)
                .ForeignKey("dbo.MPagoes", t => t.MPagoId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.DistribuidorId)
                .Index(t => t.UserId)
                .Index(t => t.MPagoId);
            
            CreateTable(
                "dbo.DetalleCompras",
                c => new
                    {
                        DetalleCompraId = c.Int(nullable: false, identity: true),
                        ProdutoId = c.Int(nullable: false),
                        DetalleCompraCantidad = c.Int(nullable: false),
                        DetalleCompraPrecio = c.Int(nullable: false),
                        DetalleCompraTotal = c.Int(nullable: false),
                        CompraId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DetalleCompraId)
                .ForeignKey("dbo.Compras", t => t.CompraId, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.ProdutoId)
                .Index(t => t.CompraId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleCompras", "ProdutoId", "dbo.Productoes");
            DropForeignKey("dbo.DetalleCompras", "CompraId", "dbo.Compras");
            DropForeignKey("dbo.Compras", "UserId", "dbo.Users");
            DropForeignKey("dbo.Compras", "MPagoId", "dbo.MPagoes");
            DropForeignKey("dbo.Compras", "DistribuidorId", "dbo.Distribuidors");
            DropIndex("dbo.DetalleCompras", new[] { "CompraId" });
            DropIndex("dbo.DetalleCompras", new[] { "ProdutoId" });
            DropIndex("dbo.Compras", new[] { "MPagoId" });
            DropIndex("dbo.Compras", new[] { "UserId" });
            DropIndex("dbo.Compras", new[] { "DistribuidorId" });
            DropTable("dbo.DetalleCompras");
            DropTable("dbo.Compras");
        }
    }
}
