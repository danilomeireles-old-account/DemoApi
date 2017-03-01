using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DemoApi.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly CustomerRepository customerRepository;

        public CustomerController()
        {
            customerRepository = new CustomerRepository();
        }               
        
        [Route("api/Customer/GetAll"), HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(customerRepository.GetAll());
        }

        [Route("api/Customer/GetAllOrderBy/{propertyName}"), HttpGet]
        public IHttpActionResult GetAllOrderBy(string propertyName)
        {
            return Ok(customerRepository.GetAllOrderBy(propertyName));
        }

        [Route("api/Customer/GetById/{id:int}"), HttpGet]
        public IHttpActionResult GetById(int id)
        {
            return Ok(customerRepository.GetById(id));
        }

        [Route("api/Customer/GetAllByPropertyILike/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByPropertyILike(string propertyName, string propertyValue)
        {
            return Ok(customerRepository.GetAllByPropertyILike(propertyName, propertyValue));
        }

        [Route("api/Customer/GetAllByProperty/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByProperty(string propertyName, string propertyValue)
        {
            return Ok(customerRepository.GetAllByProperty(propertyName, propertyValue));
        }

        [Route("api/Customer/Post"), HttpPost]
        public IHttpActionResult Post([FromBody]Customer customer)
        {
            customerRepository.Add(customer);
            customerRepository.SaveChanges();
            return Ok(customer);
        }
                
        [Route("api/Customer/Put"), HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            customerRepository.Update(customer);
            customerRepository.SaveChanges();

            return Ok(customer);
        }
                
        [Route("api/Customer/Delete/{id:int}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var customer = customerRepository.GetById(id);

            if (customer == null)
                return NotFound();

            customerRepository.Remove(customer);
            customerRepository.SaveChanges();

            return Ok(customer);
        }
    }
}