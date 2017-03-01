using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DemoApi.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly CategoryRepository categoryRepository;

        public CategoryController()
        {
            categoryRepository = new CategoryRepository();
        }               
        
        [Route("api/Category/GetAll"), HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(categoryRepository.GetAll());
        }

        [Route("api/Category/GetAllOrderBy/{propertyName}"), HttpGet]
        public IHttpActionResult GetAllOrderBy(string propertyName)
        {
            return Ok(categoryRepository.GetAllOrderBy(propertyName));
        }

        [Route("api/Category/GetById/{id:int}"), HttpGet]
        public IHttpActionResult GetById(int id)
        {
            return Ok(categoryRepository.GetById(id));
        }

        [Route("api/Category/GetAllByProperty/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByProperty(string propertyName, string propertyValue)
        {
            return Ok(categoryRepository.GetAllByProperty(propertyName, propertyValue));
        }

        [Route("api/Category/GetAllByPropertyILike/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByPropertyILike(string propertyName, string propertyValue)
        {
            return Ok(categoryRepository.GetAllByPropertyILike(propertyName, propertyValue));
        }

        [Route("api/Category/Post"), HttpPost]
        public IHttpActionResult Post([FromBody]Category category)
        {
            categoryRepository.Add(category);
            categoryRepository.SaveChanges();
            return Ok(category);
        }
                
        [Route("api/Category/Put"), HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Category category)
        {
            if (id != category.Id)
                return BadRequest();

            categoryRepository.Update(category);
            categoryRepository.SaveChanges();

            return Ok(category);
        }
                
        [Route("api/Category/Delete/{id:int}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var category = categoryRepository.GetById(id);

            if (category == null)
                return NotFound();

            categoryRepository.Remove(category);
            categoryRepository.SaveChanges();

            return Ok(category);
        }
    }
}