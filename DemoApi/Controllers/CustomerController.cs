#pragma warning disable 1591

using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace DemoApi.Controllers
{
    public class CustomerController : ApiController
    {
        /*
            https://docs.microsoft.com/en-us/azure/best-practices-api-implementation 
        */

        CustomerRepository customerRepository;

        public CustomerController()
        {
            customerRepository = new CustomerRepository();
        }

        // GET: api/Customer        
        [ResponseType(typeof(IEnumerable<Customer>))]
        public IHttpActionResult GetAll()
        {
            var customers = customerRepository.FindAll(orderByProperty: "FirstName");
            return Ok(customers); 
        }

        // GET: api/Customer/5
        [HttpGet]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult Get(int id)
        {
            var customer = customerRepository.FindById(id);

            if (customer == null)
                return NotFound();
                        
            return Ok(customer);            
        }        

        // POST: api/Customer
        [ResponseType(typeof(Customer))]
        public IHttpActionResult Post([FromBody]Customer customer)
        {
            customerRepository.Add(customer);
            customerRepository.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
        }

        // PUT: api/Customer/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult Put(int id, [FromBody]Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            customerRepository.Update(customer);
            customerRepository.SaveChanges();
            
            return Ok(customer);
        }

        // DELETE: api/Customer/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult Delete(int id)
        {
            Customer customer = customerRepository.FindById(id);

            if (customer == null)
                return NotFound();

            customerRepository.Remove(customer);
            customerRepository.SaveChanges();      

            return Ok(customer);
        }
    }
}
#pragma warning restore 1591