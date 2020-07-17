namespace C2C_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableVentas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ventas", "Buy_BuyId", c => c.Int());
            CreateIndex("dbo.Ventas", "Buy_BuyId");
            AddForeignKey("dbo.Ventas", "Buy_BuyId", "dbo.Buys", "BuyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ventas", "Buy_BuyId", "dbo.Buys");
            DropIndex("dbo.Ventas", new[] { "Buy_BuyId" });
            DropColumn("dbo.Ventas", "Buy_BuyId");
        }
    }
}
