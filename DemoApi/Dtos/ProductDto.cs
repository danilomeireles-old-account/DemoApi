using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public decimal Price { get; set; }
    }
}