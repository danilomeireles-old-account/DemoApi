#pragma warning disable 1591

using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace DemoApi.Controllers
{
    public class ProductController : ApiController
    {
        /*
            https://docs.microsoft.com/en-us/azure/best-practices-api-implementation 
        */

        ProductRepository productRepository;

        public ProductController()
        {
            productRepository = new ProductRepository();
        }

        // GET: api/Product        
        [ResponseType(typeof(IEnumerable<Product>))]
        public IHttpActionResult GetAll()
        {
            var products = productRepository.FindAll(orderByProperty: "Name");
            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get(int id)
        {
            var product = productRepository.FindById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/Product
        [ResponseType(typeof(Product))]
        public IHttpActionResult Post([FromBody]Product product)
        {
            productRepository.Add(product);
            productRepository.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // PUT: api/Product/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult Put(int id, [FromBody]Product product)
        {
            if (id != product.Id)
                return BadRequest();

            productRepository.Update(product);
            productRepository.SaveChanges();

            return Ok(product);
        }

        // DELETE: api/Product/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult Delete(int id)
        {
            Product product = productRepository.FindById(id);

            if (product == null)
                return NotFound();

            productRepository.Remove(product);
            productRepository.SaveChanges();

            return Ok(product);
        }
    }
}
#pragma warning restore 1591