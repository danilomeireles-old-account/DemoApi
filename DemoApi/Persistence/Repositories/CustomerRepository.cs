using DemoApi.Models;
using DemoApi.Persistence.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace DemoApi.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public override IEnumerable<Customer> FindAll(string orderByProperty)
        {
            return context.Customers
                .Include(c => c.ShoppingCart)
                .OrderBy(orderByProperty)
                .ToList();
        }
    }
}