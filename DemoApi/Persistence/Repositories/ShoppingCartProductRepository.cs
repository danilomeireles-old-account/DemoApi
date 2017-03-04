using DemoApi.Models;
using DemoApi.Persistence.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace DemoApi.Persistence.Repositories
{
    public class ShoppingCartProductRepository : GenericRepository<ShoppingCartProduct>
    {
        /*
            Add a method in this class
            only when the GenericRepository class
            does not solve your problem.            
            Please, do not repeat code!
        */

        public ShoppingCartProduct FindByCompositeKey(int shoppingCartId, int productId)
        {
            return entity
                .Where(scp => scp.ShoppingCartId == shoppingCartId && scp.ProductId == productId)
                .Include(scp => scp.ShoppingCart)
                .Include(scp => scp.Product)
                .SingleOrDefault();
        }
    }
}