namespace C2C_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableDistribuidor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Distribuidors",
                c => new
                    {
                        DistribuidorId = c.Int(nullable: false, identity: true),
                        RutDistribuidor = c.String(nullable: false),
                        NombreDistribuidor = c.String(nullable: false),
                        TelefonoDistribuidor = c.String(nullable: false),
                        DireccionDistribuidor = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DistribuidorId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Distribuidors");
        }
    }
}
