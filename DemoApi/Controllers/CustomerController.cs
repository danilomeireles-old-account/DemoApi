using DemoApi.Dtos;
using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System.Web.Http;

namespace DemoApi.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly CustomerRepository customerRepository;
        private readonly string[] includes;

        public CustomerController()
        {
            customerRepository = new CustomerRepository();
            includes = new string[] { "ShoppingCart" };
        }

        [Route("api/Customer/GetAll"), HttpGet]
        public IHttpActionResult GetAll()
        {
            var customers = customerRepository.GetAll(includes);
            return Ok(customers);
        }

        [Route("api/Customer/GetAllOrderBy/{propertyName}"), HttpGet]
        public IHttpActionResult GetAllOrderBy(string propertyName)
        {
            var customers = customerRepository.GetAllOrderBy(propertyName, includes);
            return Ok(customers);
        }

        [Route("api/Customer/GetById/{id:int}"), HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var customer = customerRepository.GetById(id, includes);
            return Ok(customer);
        }

        [Route("api/Customer/GetAllByProperty/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByProperty(string propertyName, string propertyValue)
        {
            var customers = customerRepository.GetAllByProperty(propertyName, propertyValue, includes);
            return Ok(customers);
        }

        [Route("api/Customer/GetAllByPropertyILike/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByPropertyILike(string propertyName, string propertyValue)
        {
            var customers = customerRepository.GetAllByPropertyILike(propertyName, propertyValue, includes);
            return Ok(customers);
        }

        [Route("api/Customer/Create"), HttpPost]
        public IHttpActionResult Create([FromBody]CustomerDto customerDto)
        {
            var customer = new Customer()
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                EmailAddress = customerDto.EmailAddress
            };

            customerRepository.Add(customer);
            customerRepository.SaveChanges();
            return Ok(customer);
        }

        [Route("api/Customer/Update"), HttpPut]
        public IHttpActionResult Update(int id, [FromBody]CustomerDto customerDto)
        {
            if (id != customerDto.Id)
                return BadRequest();

            var customer = new Customer()
            {
                Id = customerDto.Id,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                EmailAddress = customerDto.EmailAddress
            };

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