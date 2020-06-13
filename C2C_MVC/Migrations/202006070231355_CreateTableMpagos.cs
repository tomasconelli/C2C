namespace C2C_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableMpagos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MPagoes",
                c => new
                    {
                        MPagoId = c.Int(nullable: false, identity: true),
                        MPagoName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MPagoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MPagoes");
        }
    }
}
