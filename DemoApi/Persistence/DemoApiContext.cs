using DemoApi.Models;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DemoApi.Persistence
{
    public class DemoApiContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }


        public DemoApiContext() : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static DemoApiContext Create()
        {
            return new DemoApiContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("api");
            modelBuilder.Conventions.Add(new DemoApiPropertyConvention());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        private class DemoApiPropertyConvention : IStoreModelConvention<EdmProperty>
        {
            public void Apply(EdmProperty property, DbModel model)
            {
                // Uncomment and change this method if you want to apply a property convention.

                /*
                property.Name = property.Name.ToLower();
                
                if (property.Name.Contains("_id"))                
                    property.Name = "fk_" + property.Name.Replace("_id", "");                

                if (property.Name.Contains("id_"))                
                    property.Name = "pk_" + property.Name.Replace("id_", "");                
                */
            }
        }
    }
}