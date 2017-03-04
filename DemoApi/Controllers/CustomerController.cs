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
            var customers = customerRepository.GetAll();
            return Ok(customers);
        }

        [Route("api/Customer/GetAllOrderBy/{propertyName}"), HttpGet]
        public IHttpActionResult GetAllOrderBy(string propertyName)
        {
            var customers = customerRepository.GetAllOrderBy(propertyName);
            return Ok(customers);
        }

        [Route("api/Customer/GetById/{id:int}"), HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var customer = customerRepository.GetById(id);
            return Ok(customer);
        }

        [Route("api/Customer/GetAllByProperty/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByProperty(string propertyName, string propertyValue)
        {
            var customers = customerRepository.GetAllByProperty(propertyName, propertyValue);
            return Ok(customers);
        }

        [Route("api/Customer/GetAllByPropertyILike/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByPropertyILike(string propertyName, string propertyValue)
        {
            var customers = customerRepository.GetAllByPropertyILike(propertyName, propertyValue);
            return Ok(customers);
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