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
        public override IEnumerable<ShoppingCartProduct> FindAll(string orderByProperty)
        {
            return context.ShoppingCartProducts
                .Include(scp => scp.ShoppingCart)
                .Include(scp => scp.Product)
                .OrderBy(orderByProperty)
                .ToList();
        }

        public ShoppingCartProduct FindByCompositeKey(int shoppingCartId, int productId)
        {
            return context.ShoppingCartProducts
                .Where(scp => scp.ShoppingCartId == shoppingCartId && scp.ProductId == productId)
                .Include(scp => scp.ShoppingCart)
                .Include(scp => scp.Product)
                .SingleOrDefault();
        }
    }
}