using DemoApi.Models;
using DemoApi.Persistence.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace DemoApi.Persistence.Repositories
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCart>
    {
        public override IEnumerable<ShoppingCart> FindAll(string orderByProperty)
        {
            return context.ShoppingCarts
                .Include(sc => sc.Customer)
                .OrderBy(orderByProperty)
                .ToList();
        }
    }
}