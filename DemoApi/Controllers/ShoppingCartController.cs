using DemoApi.Dtos;
using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System;
using System.Web.Http;

namespace DemoApi.Controllers
{
    public class ShoppingCartController : ApiController
    {
        private readonly ShoppingCartRepository shoppingCartRepository;
        private readonly string[] includes;

        public ShoppingCartController()
        {
            shoppingCartRepository = new ShoppingCartRepository();
            includes = new string[] { "Customer" };
        }

        [Route("api/ShoppingCart/GetAll"), HttpGet]
        public IHttpActionResult GetAll()
        {
            var shoppingCarts = shoppingCartRepository.GetAll(includes);
            return Ok(shoppingCarts);
        }

        [Route("api/ShoppingCart/GetAllOrderBy/{propertyName}"), HttpGet]
        public IHttpActionResult GetAllOrderBy(string propertyName)
        {
            var shoppingCarts = shoppingCartRepository.GetAllOrderBy(propertyName, includes);
            return Ok(shoppingCarts);
        }

        [Route("api/ShoppingCart/GetById/{id:int}"), HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var shoppingCart = shoppingCartRepository.GetById("CustomerId", id, includes);
            return Ok(shoppingCart);
        }

        [Route("api/ShoppingCart/GetAllByProperty/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByProperty(string propertyName, string propertyValue)
        {
            var shoppingCarts = shoppingCartRepository.GetAllByProperty(propertyName, propertyValue, includes);
            return Ok(shoppingCarts);
        }

        [Route("api/ShoppingCart/GetAllByPropertyILike/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByPropertyILike(string propertyName, string propertyValue)
        {
            var shoppingCarts = shoppingCartRepository.GetAllByPropertyILike(propertyName, propertyValue, includes);
            return Ok(shoppingCarts);
        }

        [Route("api/ShoppingCart/Create"), HttpPost]
        public IHttpActionResult Create([FromBody]ShoppingCartPostDto shoppingCartPostDto)
        {
            var shoppingCart = new ShoppingCart();

            shoppingCart.CreationDate = DateTime.Now;
            shoppingCart.CustomerId = shoppingCartPostDto.CustomerId;

            shoppingCartRepository.Add(shoppingCart);
            shoppingCartRepository.SaveChanges();
            return Ok(shoppingCart);
        }        

        [Route("api/ShoppingCart/Delete/{id:int}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var shoppingCart = shoppingCartRepository.GetById(id);

            if (shoppingCart == null)
                return NotFound();

            shoppingCartRepository.Remove(shoppingCart);
            shoppingCartRepository.SaveChanges();
            return Ok(shoppingCart);
        }
    }
}