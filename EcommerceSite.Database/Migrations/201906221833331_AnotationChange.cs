namespace EcommerceSite.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnotationChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Description", c => c.String());
            AlterColumn("dbo.Products", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Categories", "Description", c => c.String(maxLength: 500));
        }
    }
}
