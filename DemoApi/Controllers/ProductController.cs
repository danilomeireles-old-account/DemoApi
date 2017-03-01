using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DemoApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ProductRepository productRepository;

        public ProductController()
        {
            productRepository = new ProductRepository();
        }

        [Route("api/Product/GetAll"), HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(productRepository.GetAll());
        }

        [Route("api/Product/GetAllOrderBy/{propertyName}"), HttpGet]
        public IHttpActionResult GetAllOrderBy(string propertyName)
        {
            return Ok(productRepository.GetAllOrderBy(propertyName));
        }

        [Route("api/Product/GetById/{id:int}"), HttpGet]
        public IHttpActionResult GetById(int id)
        {
            return Ok(productRepository.GetById(id));
        }

        [Route("api/Product/GetAllByProperty/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByProperty(string propertyName, string propertyValue)
        {
            return Ok(productRepository.GetAllByProperty(propertyName, propertyValue));
        }

        [Route("api/Product/GetAllByPropertyILike/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByPropertyILike(string propertyName, string propertyValue)
        {
            return Ok(productRepository.GetAllByPropertyILike(propertyName, propertyValue));
        }

        [Route("api/Product/Post"), HttpPost]
        public IHttpActionResult Post([FromBody]Product product)
        {
            productRepository.Add(product);
            productRepository.SaveChanges();
            return Ok(product);
        }

        [Route("api/Product/Put"), HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Product product)
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