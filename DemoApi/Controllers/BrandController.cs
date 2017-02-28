#pragma warning disable 1591

using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace DemoApi.Controllers
{
    public class BrandController : ApiController
    {
        /*
            https://docs.microsoft.com/en-us/azure/best-practices-api-implementation 
        */

        BrandRepository brandRepository;

        public BrandController()
        {
            brandRepository = new BrandRepository();
        }

        // GET: api/Brand        
        [ResponseType(typeof(IEnumerable<Brand>))]
        public IHttpActionResult GetAll()
        {
            var brands = brandRepository.FindAll(orderByProperty: "Name");
            return Ok(brands);
        }

        // GET: api/Brand/5
        [HttpGet]
        [ResponseType(typeof(Brand))]
        public IHttpActionResult Get(int id)
        {
            var brand = brandRepository.FindById(id);

            if (brand == null)
                return NotFound();

            return Ok(brand);
        }

        // POST: api/Brand
        [ResponseType(typeof(Brand))]
        public IHttpActionResult Post([FromBody]Brand brand)
        {
            brandRepository.Add(brand);
            brandRepository.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = brand.Id }, brand);
        }

        // PUT: api/Brand/5
        [ResponseType(typeof(Brand))]
        public IHttpActionResult Put(int id, [FromBody]Brand brand)
        {
            if (id != brand.Id)
                return BadRequest();

            brandRepository.Update(brand);
            brandRepository.SaveChanges();

            return Ok(brand);
        }

        // DELETE: api/Brand/5
        [ResponseType(typeof(Brand))]
        public IHttpActionResult Delete(int id)
        {
            Brand brand = brandRepository.FindById(id);

            if (brand == null)
                return NotFound();

            brandRepository.Remove(brand);
            brandRepository.SaveChanges();

            return Ok(brand);
        }
    }
}
#pragma warning restore 1591