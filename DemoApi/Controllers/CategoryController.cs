#pragma warning disable 1591

using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace DemoApi.Controllers
{
    public class CategoryController : ApiController
    {
        /*
            https://docs.microsoft.com/en-us/azure/best-practices-api-implementation 
        */

        CategoryRepository categoryRepository;

        public CategoryController()
        {
            categoryRepository = new CategoryRepository();
        }

        // GET: api/Category        
        [ResponseType(typeof(IEnumerable<Category>))]
        public IHttpActionResult GetAll()
        {
            var categories = categoryRepository.FindAll(orderByProperty: "Name");
            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet]
        [ResponseType(typeof(Category))]
        public IHttpActionResult Get(int id)
        {
            var category = categoryRepository.FindById(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // POST: api/Category
        [ResponseType(typeof(Category))]
        public IHttpActionResult Post([FromBody]Category category)
        {
            categoryRepository.Add(category);
            categoryRepository.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        }

        // PUT: api/Category/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult Put(int id, [FromBody]Category category)
        {
            if (id != category.Id)
                return BadRequest();

            categoryRepository.Update(category);
            categoryRepository.SaveChanges();

            return Ok(category);
        }

        // DELETE: api/Category/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult Delete(int id)
        {
            Category category = categoryRepository.FindById(id);

            if (category == null)
                return NotFound();

            categoryRepository.Remove(category);
            categoryRepository.SaveChanges();

            return Ok(category);
        }
    }
}
#pragma warning restore 1591