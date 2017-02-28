#pragma warning disable 1591

using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace DemoApi.Controllers
{
    public class ShoppingCartController : ApiController
    {
        /*
            https://docs.microsoft.com/en-us/azure/best-practices-api-implementation 
        */

        ShoppingCartRepository shoppingCartRepository;

        public ShoppingCartController()
        {
            shoppingCartRepository = new ShoppingCartRepository();
        }

        // GET: api/ShoppingCart        
        [ResponseType(typeof(IEnumerable<ShoppingCart>))]
        public IHttpActionResult GetAll()
        {
            var shoppingCarts = shoppingCartRepository.FindAll(orderByProperty: "CreationDate");
            return Ok(shoppingCarts);
        }

        // GET: api/ShoppingCart/5
        [HttpGet]
        [ResponseType(typeof(ShoppingCart))]
        public IHttpActionResult Get(int id)
        {
            var shoppingCart = shoppingCartRepository.FindById(id);

            if (shoppingCart == null)
                return NotFound();

            return Ok(shoppingCart);
        }

        // POST: api/ShoppingCart
        [ResponseType(typeof(ShoppingCart))]
        public IHttpActionResult Post([FromBody]ShoppingCart shoppingCart)
        {
            shoppingCartRepository.Add(shoppingCart);
            shoppingCartRepository.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = shoppingCart.CustomerId }, shoppingCart);
        }

        // PUT: api/ShoppingCart/5
        [ResponseType(typeof(ShoppingCart))]
        public IHttpActionResult Put(int id, [FromBody]ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.CustomerId)
                return BadRequest();

            shoppingCartRepository.Update(shoppingCart);
            shoppingCartRepository.SaveChanges();

            return Ok(shoppingCart);
        }

        // DELETE: api/ShoppingCart/5
        [ResponseType(typeof(ShoppingCart))]
        public IHttpActionResult Delete(int id)
        {
            ShoppingCart shoppingCart = shoppingCartRepository.FindById(id);

            if (shoppingCart == null)
                return NotFound();

            shoppingCartRepository.Remove(shoppingCart);
            shoppingCartRepository.SaveChanges();

            return Ok(shoppingCart);
        }
    }
}
#pragma warning restore 1591