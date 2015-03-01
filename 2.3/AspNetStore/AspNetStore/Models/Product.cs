using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}