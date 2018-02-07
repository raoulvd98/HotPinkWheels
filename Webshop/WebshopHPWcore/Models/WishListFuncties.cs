using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class WishListFuncties
    {
        private readonly ShopContext _dbContext;
        private readonly string _wishListItemId;
        private readonly string _userId;

        private WishListFuncties(ShopContext dbContext, string id, string UserId)
        {
            _dbContext = dbContext;
            _wishListItemId = id;
            _userId = UserId;
        }

        public static WishListFuncties GetCart(ShopContext db, HttpContext context, string UserId)
            => GetCart(db, "", UserId);
        
        public static WishListFuncties GetCart(ShopContext db, string cartId, string UserId)
            => new WishListFuncties(db, cartId, UserId);

        public async Task AddToWishList(Car car)
        {
            var cartItem = await _dbContext.WishListItems.SingleOrDefaultAsync(
                c => c.WishListItemId == _wishListItemId
                && c.carid == car.carid);

            if (cartItem == null)
            {
                cartItem = new WishListItem
                {
                    userid = _userId,
                    WishListItemId = GetWishListItemId(),
                    carid = car.carid,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                _dbContext.WishListItems.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
        }

        public int RemoveFromWishList(string id)
        {
            // Get the cart
            var wishListItem = _dbContext.WishListItems.SingleOrDefault(
                cart => cart.WishListItemId == id);

            int itemCount = 0;

            if (wishListItem != null)
            {
                if (wishListItem.Count > 1)
                {
                    wishListItem.Count--;
                    itemCount = wishListItem.Count;
                }
                else
                {
                    _dbContext.WishListItems.Remove(wishListItem);
                }
            }
            return itemCount;
        }

        public Task<List<WishListItem>> GetWishListItems()
        {
            if (_userId != null)
            {
                return _dbContext
                .WishListItems
                .Where(cart => cart.userid == _userId)
                .Include(c => c.Car)
                .ToListAsync();
            }

            return _dbContext
                .WishListItems
                .Where(cart => cart.WishListItemId == _wishListItemId)
                .Include(c => c.Car)
                .ToListAsync();
        }
        
        private static string GetWishListItemId()
        {
            string WishListItemId;

            //A GUID to hold the CartItemId. 
            WishListItemId = Guid.NewGuid().ToString();
            

            return WishListItemId;
        }
    }
}
