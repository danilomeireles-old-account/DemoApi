using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DemoApi.Controllers
{
    public class BrandController : ApiController
    {
        private readonly BrandRepository brandRepository;

        public BrandController()
        {
            brandRepository = new BrandRepository();
        }               
        
        [Route("api/Brand/GetAll"), HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(brandRepository.GetAll());
        }

        [Route("api/Brand/GetAllOrderBy/{propertyName}"), HttpGet]
        public IHttpActionResult GetAllOrderBy(string propertyName)
        {
            return Ok(brandRepository.GetAllOrderBy(propertyName));
        }

        [Route("api/Brand/GetById/{id:int}"), HttpGet]
        public IHttpActionResult GetById(int id)
        {
            return Ok(brandRepository.GetById(id));
        }

        [Route("api/Brand/GetAllByProperty/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByProperty(string propertyName, string propertyValue)
        {
            return Ok(brandRepository.GetAllByProperty(propertyName, propertyValue));
        }

        [Route("api/Brand/GetAllByPropertyILike/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByPropertyILike(string propertyName, string propertyValue)
        {
            return Ok(brandRepository.GetAllByPropertyILike(propertyName, propertyValue));
        }

        [Route("api/Brand/Post"), HttpPost]
        public IHttpActionResult Post([FromBody]Brand brand)
        {
            brandRepository.Add(brand);
            brandRepository.SaveChanges();
            return Ok(brand);
        }
                
        [Route("api/Brand/Put"), HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Brand brand)
        {
            if (id != brand.Id)
                return BadRequest();

            brandRepository.Update(brand);
            brandRepository.SaveChanges();

            return Ok(brand);
        }
                
        [Route("api/Brand/Delete/{id:int}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var brand = brandRepository.GetById(id);

            if (brand == null)
                return NotFound();

            brandRepository.Remove(brand);
            brandRepository.SaveChanges();

            return Ok(brand);
        }
    }
}