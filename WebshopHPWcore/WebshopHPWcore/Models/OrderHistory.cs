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
    public class OrderHistory
    {
        private readonly ShopContext _dbContext;
        private readonly string _shoppingCartId;
        private readonly string _userId;

        private OrderHistory(ShopContext dbContext, string id, string UserId)
        {
            _dbContext = dbContext;
            _shoppingCartId = id;
            _userId = UserId;
        }

        public static OrderHistory GetCart(ShopContext db, HttpContext context, string UserId)
        => GetCart(db, "", UserId);

        public static OrderHistory GetCart(ShopContext db, string cartId, string UserId)
            => new OrderHistory(db, cartId, UserId);

        public Task<List<History>> GetHistoryItems()
        {
            return _dbContext
                   .History
                   .Where(car => car.userid == _userId)
                   .Include(c => c.Car)
                   .ToListAsync();
        }

        public Task<List<Orderdetail>> GetOrderDetails()
        {
            return _dbContext
                   .orderdetail
                   .Where(car => car.Userid == _userId)
                   .ToListAsync();
        }
    }
}
