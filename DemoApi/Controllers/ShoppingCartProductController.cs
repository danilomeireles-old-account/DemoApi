using DemoApi.Dtos;
using DemoApi.Models;
using DemoApi.Persistence.Repositories;
using System.Web.Http;

namespace DemoApi.Controllers
{
    public class ShoppingCartProductController : ApiController
    {
        private readonly ShoppingCartProductRepository shoppingCartProductRepository;
        private readonly string[] includes;

        public ShoppingCartProductController()
        {
            shoppingCartProductRepository = new ShoppingCartProductRepository();
            includes = new string[] { "ShoppingCart", "ShoppingCart.Customer", "Product", "Product.Brand", "Product.Category" };
        }

        [Route("api/ShoppingCartProduct/GetAll"), HttpGet]
        public IHttpActionResult GetAll()
        {
            var shoppingCartProducts = shoppingCartProductRepository.GetAll(includes);
            return Ok(shoppingCartProducts);
        }

        [Route("api/ShoppingCartProduct/GetAllOrderBy/{propertyName}"), HttpGet]
        public IHttpActionResult GetAllOrderBy(string propertyName)
        {
            var shoppingCartProducts = shoppingCartProductRepository.GetAllOrderBy(propertyName, includes);
            return Ok(shoppingCartProducts);
        }

        [Route("api/ShoppingCartProduct/GetByCompositeKey/{shoppingCartId:int}/{productId:int}"), HttpGet]
        public IHttpActionResult GetByCompositeKey(int shoppingCartId, int productId)
        {
            var shoppingCartProduct = shoppingCartProductRepository.GetByCompositeKey(shoppingCartId, productId);
            return Ok(shoppingCartProduct);
        }

        [Route("api/ShoppingCartProduct/GetAllByProperty/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByProperty(string propertyName, string propertyValue)
        {
            var shoppingCartProducts = shoppingCartProductRepository.GetAllByProperty(propertyName, propertyValue, includes);
            return Ok(shoppingCartProducts);
        }

        [Route("api/ShoppingCartProduct/GetAllByPropertyILike/{propertyName}/{propertyValue}"), HttpGet]
        public IHttpActionResult GetAllByPropertyILike(string propertyName, string propertyValue)
        {
            var shoppingCartProducts = shoppingCartProductRepository.GetAllByPropertyILike(propertyName, propertyValue, includes);
            return Ok(shoppingCartProducts);
        }

        [Route("api/ShoppingCartProduct/Create"), HttpPost]
        public IHttpActionResult Create([FromBody]ShoppingCartProductDto shoppingCartProductDto)
        {
            var shoppingCartProduct = new ShoppingCartProduct()
            {
                ShoppingCartId = shoppingCartProductDto.ShoppingCartId,
                ProductId = shoppingCartProductDto.ProductId,
                Quantity = shoppingCartProductDto.Quantity
            };

            shoppingCartProductRepository.Add(shoppingCartProduct);
            shoppingCartProductRepository.SaveChanges();
            return Ok(shoppingCartProduct);
        }

        [Route("api/Update/{shoppingCartId:int}/{productId:int}"), HttpPut]
        public IHttpActionResult Update(int shoppingCartId, int productId, [FromBody]ShoppingCartProductDto shoppingCartProductDto)
        {
            if (shoppingCartId != shoppingCartProductDto.ShoppingCartId || productId != shoppingCartProductDto.ProductId)
                return BadRequest();

            var shoppingCartProduct = new ShoppingCartProduct()
            {                
                ShoppingCartId = shoppingCartProductDto.ShoppingCartId,
                ProductId = shoppingCartProductDto.ProductId,
                Quantity = shoppingCartProductDto.Quantity
            };

            shoppingCartProductRepository.Update(shoppingCartProduct);
            shoppingCartProductRepository.SaveChanges();
            return Ok(shoppingCartProduct);
        }        

        [Route("api/ShoppingCartProduct/Delete/ShoppingCart/{shoppingCartId:int}/Product/{productId:int}"), HttpDelete]
        public IHttpActionResult Delete(int shoppingCartId, int productId)
        {
            var shoppingCartProduct = shoppingCartProductRepository.GetById(shoppingCartId, productId);

            if (shoppingCartProduct == null)
                return NotFound();

            shoppingCartProductRepository.Remove(shoppingCartProduct);
            shoppingCartProductRepository.SaveChanges();
            return Ok(shoppingCartProduct);
        }
    }
}