namespace EcommerceSite.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderAndOrderItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        OderedAt = c.DateTime(nullable: false),
                        Status = c.String(),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "ProductID" });
            DropIndex("dbo.OrderItems", new[] { "OrderID" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
        }
    }
}
