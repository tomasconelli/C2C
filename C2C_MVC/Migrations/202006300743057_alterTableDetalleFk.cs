namespace C2C_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTableDetalleFk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DetalleVentas", "VentaId", "dbo.Ventas");
            DropIndex("dbo.DetalleVentas", new[] { "VentaId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.DetalleVentas", "VentaId");
            AddForeignKey("dbo.DetalleVentas", "VentaId", "dbo.Ventas", "VentaId", cascadeDelete: true);
        }
    }
}
