using DemoApi.Models;
using DemoApi.Persistence.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace DemoApi.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public override IEnumerable<Product> FindAll(string orderByProperty)
        {
            return context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .OrderBy(orderByProperty)
                .ToList();                
        }
    }
}