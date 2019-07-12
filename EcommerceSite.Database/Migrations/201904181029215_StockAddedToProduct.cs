namespace EcommerceSite.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockAddedToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Stock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Stock");
        }
    }
}
