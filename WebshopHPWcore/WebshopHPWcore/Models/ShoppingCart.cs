using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WebshopHPWcore.Models
{
    public class ShoppingCart
    {
        private readonly ShopContext _dbContext;
        private readonly string _shoppingCartId;
        private readonly string _userId;

        private ShoppingCart(ShopContext dbContext, string id, string UserId)
        {
            _dbContext = dbContext;
            _shoppingCartId = id;
            _userId = UserId;
        }

        private ShoppingCart(ShopContext dbContext, string id)
        {
            _dbContext = dbContext;
            _shoppingCartId = id;
        }

        public static ShoppingCart GetCart(ShopContext db, HttpContext context)
            => GetCart(db, GetCartId(context));

        public static ShoppingCart GetCart(ShopContext db, HttpContext context, string UserId)
            => GetCart(db, "", UserId);

        public static ShoppingCart GetCart(ShopContext db, string cartId)
           => new ShoppingCart(db, cartId);

        public static ShoppingCart GetCart(ShopContext db, string cartId, string UserId)
            => new ShoppingCart(db, cartId, UserId);

        public async Task AddToCart(Car car)
        {
            var cartItem = await _dbContext.CartItems.SingleOrDefaultAsync(
                c => c.CartId == _shoppingCartId
                && c.carid == car.carid);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    userid = _userId,
                    CartItemId = GetCartItemId(),
                    carid = car.carid,
                    CartId = _shoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                _dbContext.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Count = 1;
            }
        }

        public async Task CountDecrease(List<CartItem> items)
        {
            foreach (var item in items)
            {
                var id = item.carid;
                var query = (from cars in _dbContext.cars
                             where cars.carid == id
                             select cars).First();
                query.Count -= 1;                
            }
            _dbContext.SaveChanges();
        }

        //public async Task AddToOrderDetails(Orderdetail model, List<CartItem> cartitems)
        //{
        //    var OrderId = GetCartItemId();

        //        var details = new Orderdetail
        //        {
        //            Address = model.Address,
        //            Email = model.Email,
        //            Middlename = model.Middlename,
        //            Lastname = model.Lastname,
        //            Firstname = model.Firstname,
        //            Housenumber = model.Housenumber,
        //            Phonenumber = model.Phonenumber,
        //            Zipcode = model.Zipcode,
        //            Orderid = OrderId
        //        };
        //    _dbContext.orderdetail.Add(details);

        //    foreach (var item in cartitems)
        //    {

        //        var cartItem = await _dbContext.History.SingleOrDefaultAsync(
        //            c => c.userid == _userId
        //            && c.carid == item.carid);


        //        if (cartItem == null)
        //        {
        //            cartItem = new History
        //            {
        //                userid = item.userid,
        //                CartItemId = item.CartItemId,
        //                carid = item.carid,
        //                Count = item.Count,
        //                OrderId = OrderId,
        //                DateCreated = DateTime.Now
        //            };
        //            _dbContext.History.Add(cartItem);

        //        }
        //        _dbContext.SaveChanges();

        //    }

        //}

        public async Task AddToHistory(List<CartItem> items, string id, string usertid)
        {
            foreach (var item in items)
            {
                var cartItem = await _dbContext.History.SingleOrDefaultAsync(
                    c => c.userid == _userId
                    && c.carid == item.carid);

                if (cartItem == null)
                {
                    cartItem = new History
                    {
                        userid = usertid,
                        CartItemId = item.CartItemId,
                        carid = item.carid,
                        Count = item.Count,
                        OrderId = id,
                        DateCreated = DateTime.Now,
                        Price = GetTotal(item.carid)
                    };
                    _dbContext.History.Add(cartItem);

                }
                _dbContext.SaveChanges();
            }
            
        }

        public int RemoveFromCart(string id)
        {
            var cartItem = _dbContext.CartItems.SingleOrDefault(
                cart => cart.CartId == _shoppingCartId
                && cart.CartItemId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _dbContext.CartItems.Remove(cartItem);
                }
                _dbContext.SaveChanges();
            }
            return itemCount;
        } 

        public Task<List<CartItem>> GetCartItems()
        {
            if (_userId != null)
            {
                return _dbContext
                .CartItems
                .Where(cart => cart.userid == _userId)
                .Include(c => c.Car)
                .ToListAsync();
            }

            return _dbContext
                .CartItems
                .Where(cart => cart.CartId == _shoppingCartId)
                .Include(c => c.Car)
                .ToListAsync();
        }
        
        private static string GetCartId(HttpContext context)
        {
            string cartId = context.Session.GetString("Session");

            if (cartId == null)
            {
                cartId = Guid.NewGuid().ToString();
                
                context.Session.SetString("Session", cartId);
            }
            return cartId;
        }

        public async Task EmptyCart()
        {
            var cartItems = await _dbContext
                .CartItems
                .Where(cart => cart.CartId == _shoppingCartId)
                .ToArrayAsync();

            _dbContext.CartItems.RemoveRange(cartItems);
        }

        public Task<int> GetCount()
        {
            return _dbContext
                .CartItems
                .Where(c => c.CartId == _shoppingCartId)
                .Select(c => c.Count)
                .SumAsync();
        }

        public static string GetCartItemId()
        {
            string CartItemId;
            
            CartItemId = Guid.NewGuid().ToString();

            return CartItemId;
        }

        public Task<decimal> GetTotal()
        {
            if (_userId != null)
            {
                return _dbContext
                 .CartItems
                 .Where(c => c.userid == _userId)
                 .Select(c => c.Car.price * c.Count)
                 .SumAsync();
            }

            return _dbContext
                .CartItems
                .Where(c => c.CartId == _shoppingCartId)
                .Select(c => c.Car.price * c.Count)
                .SumAsync();
        }

        public decimal GetTotal(int carid)
        {
            return _dbContext
                .cars
                .Where(car => car.carid == carid)
                .Select(c => c.price)
                .First();
        }
    }
}
