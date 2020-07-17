namespace C2C_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableUserSexo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SexoUser", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "SexoUser");
        }
    }
}
