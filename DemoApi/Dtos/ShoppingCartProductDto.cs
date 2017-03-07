using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.Dtos
{
    public class ShoppingCartProductDto
    {
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}