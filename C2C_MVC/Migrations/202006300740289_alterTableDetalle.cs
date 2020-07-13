namespace C2C_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTableDetalle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DetalleVentas", "DetalleVentaObs", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DetalleVentas", "DetalleVentaObs");
        }
    }
}
