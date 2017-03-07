using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DemoApi.Controllers
{
    public class ConfigController : ApiController
    {
        private readonly BrandRepository brandRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly ProductRepository productRepository;
        private readonly CustomerRepository customerRepository;
        private readonly ShoppingCartRepository shoppingCartRepository;
        private readonly ShoppingCartProductRepository shoppingCartProductRepository;

        public ConfigController()
        {
            brandRepository = new BrandRepository();
            categoryRepository = new CategoryRepository();
            productRepository = new ProductRepository();
            customerRepository = new CustomerRepository();
            shoppingCartRepository = new ShoppingCartRepository();
            shoppingCartProductRepository = new ShoppingCartProductRepository();
        }

        /// <summary>
        /// Call this service to reset API data.
        /// </summary>
        /// <returns></returns>
        [Route("api/Config/ResetApiData"), HttpGet]
        public IHttpActionResult ResetApiData()
        {
            // Delete all data:
            shoppingCartProductRepository.RemoveRange(shoppingCartProductRepository.GetAll());
            shoppingCartProductRepository.SaveChanges();

            shoppingCartRepository.RemoveRange(shoppingCartRepository.GetAll());
            shoppingCartRepository.SaveChanges();

            customerRepository.RemoveRange(customerRepository.GetAll());
            customerRepository.SaveChanges();

            productRepository.RemoveRange(productRepository.GetAll());
            productRepository.SaveChanges();

            brandRepository.RemoveRange(brandRepository.GetAll());
            brandRepository.SaveChanges();

            categoryRepository.RemoveRange(categoryRepository.GetAll());
            categoryRepository.SaveChanges();

            // Create brands:
            List<Brand> brands = new List<Brand>();
            brands.Add(new Brand() { Name = "Samsung" });
            brands.Add(new Brand() { Name = "Lenovo" });
            brands.Add(new Brand() { Name = "Dell" });
            brands.Add(new Brand() { Name = "Asus" });
            brands.Add(new Brand() { Name = "HP" });
            brands.Add(new Brand() { Name = "Acer" });
            brands.Add(new Brand() { Name = "Sony" });
            brands.Add(new Brand() { Name = "Microsoft" });
            brandRepository.AddRange(brands);
            brandRepository.SaveChanges();

            // Create categories:
            List<Category> categories = new List<Category>();
            categories.Add(new Category() { Name = "Cellphones" });
            categories.Add(new Category() { Name = "Computers" });
            categories.Add(new Category() { Name = "Tablets" });
            categories.Add(new Category() { Name = "Video Games" });
            categories.Add(new Category() { Name = "Accessories" });
            categoryRepository.AddRange(categories);
            categoryRepository.SaveChanges();

            // Create products:
            List<Product> products = new List<Product>();
            products.Add(new Product() { Name = "Samsung Galaxy S8", BrandId = brands[0].Id, CategoryId = categories[0].Id, Price = Convert.ToDecimal(789.99) });
            products.Add(new Product() { Name = "Dell XPS 13", BrandId = brands[2].Id, CategoryId = categories[1].Id, Price = Convert.ToDecimal(1144.99) });
            products.Add(new Product() { Name = "Samsung Galaxy Tab", BrandId = brands[0].Id, CategoryId = categories[2].Id, Price = Convert.ToDecimal(989.59) });
            products.Add(new Product() { Name = "Playstation 4", BrandId = brands[6].Id, CategoryId = categories[3].Id, Price = Convert.ToDecimal(299.99) });
            products.Add(new Product() { Name = "Microsoft Surface Mouse", BrandId = brands[7].Id, CategoryId = categories[4].Id, Price = Convert.ToDecimal(64.99) });
            productRepository.AddRange(products);
            productRepository.SaveChanges();

            // Create customers:
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer() { FirstName = "Adam", LastName = "Smith", EmailAddress = "adam.smith@demo.com" });
            customers.Add(new Customer() { FirstName = "Arnold", LastName = "Jordan", EmailAddress = "arnold.jordan@demo.com" });
            customers.Add(new Customer() { FirstName = "Calvin", LastName = "Williams", EmailAddress = "calvin.williams@demo.com" });
            customers.Add(new Customer() { FirstName = "Emma", LastName = "Jones", EmailAddress = "emma.jones@demo.com" });
            customers.Add(new Customer() { FirstName = "Sophia", LastName = "Wilson", EmailAddress = "sophia.wilson@demo.com" });
            customerRepository.AddRange(customers);
            customerRepository.SaveChanges();

            return Ok("Api data reseted.");
        }
    }
}
