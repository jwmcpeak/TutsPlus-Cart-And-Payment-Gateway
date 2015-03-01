using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetStore.Models;

namespace AspNetStore.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<CartItem> Items { get; set; }
        public decimal Total { get; set; }
    }
}