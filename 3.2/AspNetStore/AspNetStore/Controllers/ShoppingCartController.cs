using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AspNetStore.Services;
using AspNetStore.Models;
using AspNetStore.ViewModels;

namespace AspNetStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public async Task<ActionResult> Index()
        {
            var cart = new ShoppingCart(HttpContext);
            var items = await cart.GetCartItemsAsync();

            return View(new ShoppingCartViewModel
            {
                Items = items,
                Total = CalcuateCart(items)
            });
        }

        public async Task<ActionResult> AddToCart(int id)
        {
            var cart = new ShoppingCart(HttpContext);

            await cart.AddAsync(id);

            return RedirectToAction("index");
        }

        public async Task<ActionResult> RemoveFromCart(int id)
        {
            var cart = new ShoppingCart(HttpContext);

            await cart.RemoveAsync(id);

            return RedirectToAction("index");
        }

        private static decimal CalcuateCart(IEnumerable<CartItem> items)
        {
            return items.Sum(item => (item.Product.Price*item.Count));
        }
    }
}