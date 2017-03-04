using DemoApi.Dtos;
using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Omu.ValueInjecter;

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
            var categories = categoryRepository.GetAll();
            return Ok(categories);
        }

        [Route("api/Category/GetAllOrderBy/{propertyName}"), HttpGet]
        public IHttpActionResult GetAllOrderBy(string propertyName)
        {
            var categories = categoryRepository.GetAllOrderBy(propertyName);
            return Ok(categories);
        }

        [Route("api/Category/GetById/{id:int}"), HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var category = categoryRepository.GetById(id);
            return Ok(category);
        }

        [Route("api/Category/GetAllByProperty/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByProperty(string propertyName, string propertyValue)
        {
            var categories = categoryRepository.GetAllByProperty(propertyName, propertyValue);
            return Ok(categories);
        }

        [Route("api/Category/GetAllByPropertyILike/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByPropertyILike(string propertyName, string propertyValue)
        {
            var categories = categoryRepository.GetAllByPropertyILike(propertyName, propertyValue);
            return Ok(categories);
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