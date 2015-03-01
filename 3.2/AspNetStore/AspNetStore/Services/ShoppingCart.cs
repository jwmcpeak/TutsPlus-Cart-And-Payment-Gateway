using AspNetStore.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AspNetStore.Models;

namespace AspNetStore.Services
{
    public class ShoppingCart
    {
        private readonly StoreContext _db;
        private readonly string _cartId;

        public ShoppingCart(HttpContextBase context) 
            : this(context, new StoreContext())
        {
        }

        public ShoppingCart(HttpContextBase httpContext, StoreContext storeContext)
        {
            _db = storeContext;
            _cartId = GetCartId(httpContext);
        }

        public async Task AddAsync(int productId)
        {
            var product = await _db.Products
                .SingleOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                // TODO: throw exception or do something
                return;
            }

            var cartItem = await _db.CartItems
                .SingleOrDefaultAsync(c => c.ProductId == productId && c.CartId == _cartId);

            if (cartItem != null)
            {
                cartItem.Count++;
            }
            else
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    CartId = _cartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                _db.CartItems.Add(cartItem);
            }

            await _db.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(int productId)
        {
            var cartItem = await _db.CartItems
                .SingleOrDefaultAsync(c => c.ProductId == productId && c.CartId == _cartId);

            var itemCount = 0;

            if (cartItem == null)
            {
                return itemCount;
            }

            if (cartItem.Count > 1)
            {
                cartItem.Count--;
                itemCount = cartItem.Count;
            }
            else
            {
                _db.CartItems.Remove(cartItem);
            }

            await _db.SaveChangesAsync();

            return itemCount;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync()
        {
            return await _db.CartItems.Include("Product")
                .Where(c => c.CartId == _cartId).ToArrayAsync();
        }

        private string GetCartId(HttpContextBase http)
        {
            var cookie = http.Request.Cookies.Get("ShoppingCart");
            var cartId = string.Empty;

            if (cookie == null || string.IsNullOrWhiteSpace(cookie.Value))
            {
                cookie = new HttpCookie("ShoppingCart");
                cartId = Guid.NewGuid().ToString();

                cookie.Value = cartId;
                cookie.Expires = DateTime.Now.AddDays(7);

                http.Response.Cookies.Add(cookie);
            }
            else
            {
                cartId = cookie.Value;
            }

            return cartId;
        }

    }
}