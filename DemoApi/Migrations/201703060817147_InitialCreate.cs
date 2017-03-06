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
                "api.ShoppingCart",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("api.Customer", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
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
                    })
                .PrimaryKey(t => new { t.ShoppingCartId, t.ProductId })
                .ForeignKey("api.Product", t => t.ProductId)
                .ForeignKey("api.ShoppingCart", t => t.ShoppingCartId)
                .Index(t => t.ShoppingCartId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("api.ShoppingCartProduct", "ShoppingCartId", "api.ShoppingCart");
            DropForeignKey("api.ShoppingCartProduct", "ProductId", "api.Product");
            DropForeignKey("api.Product", "CategoryId", "api.Category");
            DropForeignKey("api.Product", "BrandId", "api.Brand");
            DropForeignKey("api.ShoppingCart", "CustomerId", "api.Customer");
            DropIndex("api.ShoppingCartProduct", new[] { "ProductId" });
            DropIndex("api.ShoppingCartProduct", new[] { "ShoppingCartId" });
            DropIndex("api.Product", new[] { "BrandId" });
            DropIndex("api.Product", new[] { "CategoryId" });
            DropIndex("api.ShoppingCart", new[] { "CustomerId" });
            DropIndex("api.Customer", "unique_email");
            DropIndex("api.Category", "unique_name");
            DropIndex("api.Brand", "unique_name");
            DropTable("api.ShoppingCartProduct");
            DropTable("api.Product");
            DropTable("api.ShoppingCart");
            DropTable("api.Customer");
            DropTable("api.Category");
            DropTable("api.Brand");
        }
    }
}
