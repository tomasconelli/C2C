namespace C2C_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrateTableBuyAndBuyProducts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyProductoes",
                c => new
                    {
                        BuyProductoId = c.Int(nullable: false, identity: true),
                        BuyId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Precio = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BuyProductoId)
                .ForeignKey("dbo.Buys", t => t.BuyId, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.BuyId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Buys",
                c => new
                    {
                        BuyId = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.BuyId)
                .ForeignKey("dbo.Users", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuyProductoes", "ProdutoId", "dbo.Productoes");
            DropForeignKey("dbo.BuyProductoes", "BuyId", "dbo.Buys");
            DropForeignKey("dbo.Buys", "AuthorId", "dbo.Users");
            DropIndex("dbo.Buys", new[] { "AuthorId" });
            DropIndex("dbo.BuyProductoes", new[] { "ProdutoId" });
            DropIndex("dbo.BuyProductoes", new[] { "BuyId" });
            DropTable("dbo.Buys");
            DropTable("dbo.BuyProductoes");
        }
    }
}
