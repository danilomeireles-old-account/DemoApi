namespace DemoApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "api.Brand",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "unique_name");
            
            CreateTable(
                "api.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "unique_name");
            
            CreateTable(
                "api.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        EmailAddress = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.EmailAddress, unique: true, name: "unique_email");
            
            CreateTable(
                "api.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("api.Brand", t => t.BrandId)
                .ForeignKey("api.Category", t => t.CategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "api.ShoppingCartProduct",
                c => new
                    {
                        ShoppingCartId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ShoppingCart_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => new { t.ShoppingCartId, t.ProductId })
                .ForeignKey("api.Product", t => t.ProductId)
                .ForeignKey("api.ShoppingCart", t => t.ShoppingCart_CustomerId)
                .Index(t => t.ProductId)
                .Index(t => t.ShoppingCart_CustomerId);
            
            CreateTable(
                "api.ShoppingCart",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("api.Customer", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("api.ShoppingCartProduct", "ShoppingCart_CustomerId", "api.ShoppingCart");
            DropForeignKey("api.ShoppingCart", "CustomerId", "api.Customer");
            DropForeignKey("api.ShoppingCartProduct", "ProductId", "api.Product");
            DropForeignKey("api.Product", "CategoryId", "api.Category");
            DropForeignKey("api.Product", "BrandId", "api.Brand");
            DropIndex("api.ShoppingCart", new[] { "CustomerId" });
            DropIndex("api.ShoppingCartProduct", new[] { "ShoppingCart_CustomerId" });
            DropIndex("api.ShoppingCartProduct", new[] { "ProductId" });
            DropIndex("api.Product", new[] { "BrandId" });
            DropIndex("api.Product", new[] { "CategoryId" });
            DropIndex("api.Customer", "unique_email");
            DropIndex("api.Category", "unique_name");
            DropIndex("api.Brand", "unique_name");
            DropTable("api.ShoppingCart");
            DropTable("api.ShoppingCartProduct");
            DropTable("api.Product");
            DropTable("api.Customer");
            DropTable("api.Category");
            DropTable("api.Brand");
        }
    }
}
