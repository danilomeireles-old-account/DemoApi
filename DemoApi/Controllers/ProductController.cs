using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System.Web.Http;

namespace DemoApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ProductRepository productRepository;
        private readonly string[] includes;

        public ProductController()
        {
            productRepository = new ProductRepository();
            includes = new string[] { "Category", "Brand" };
        }

        [Route("api/Product/GetAll"), HttpGet]
        public IHttpActionResult GetAll()
        {
            var products = productRepository.GetAll(includes);
            return Ok(products);
        }

        [Route("api/Product/GetAllOrderBy/{propertyName}"), HttpGet]
        public IHttpActionResult GetAllOrderBy(string propertyName)
        {
            var products = productRepository.GetAllOrderBy(propertyName, includes);
            return Ok(products);
        }

        [Route("api/Product/GetById/{id:int}"), HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var product = productRepository.GetById(id, includes);
            return Ok(product);
        }

        [Route("api/Product/GetAllByProperty/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByProperty(string propertyName, string propertyValue)
        {
            var products = productRepository.GetAllByProperty(propertyName, propertyValue, includes);
            return Ok(products);
        }

        [Route("api/Product/GetAllByPropertyILike/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByPropertyILike(string propertyName, string propertyValue)
        {
            var products = productRepository.GetAllByPropertyILike(propertyName, propertyValue, includes);
            return Ok(products);
        }

        [Route("api/Product/Create"), HttpPost]
        public IHttpActionResult Create([FromBody]Product product)
        {
            productRepository.Add(product);
            productRepository.SaveChanges();
            return Ok(product);
        }

        [Route("api/Product/Update"), HttpPut]
        public IHttpActionResult Update(int id, [FromBody]Product product)
        {
            if (id != product.Id)
                return BadRequest();

            productRepository.Update(product);
            productRepository.SaveChanges();
            return Ok(product);
        }

        [Route("api/Product/Delete/{id:int}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var product = productRepository.GetById(id);

            if (product == null)
                return NotFound();

            productRepository.Remove(product);
            productRepository.SaveChanges();
            return Ok(product);
        }
    }
}