using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebshopHPWcore.Models;
using WebshopHPWcore.Models.HistoryViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebshopHPWcore.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ILogger<HistoryController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShopContext DbContext { get; }

        public HistoryController(ShopContext dbContext, ILogger<HistoryController> logger,
    SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            DbContext = dbContext;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            OrderHistory cart;
            cart = UserLogin();

            var viewModel = new HistoryViewModel
            {
                HistoryItems = await cart.GetHistoryItems(),
                orderdetail = await cart.GetOrderDetails()
            };
            return View(viewModel);
        }
        public OrderHistory UserLogin()
        {
            OrderHistory cart;

            if (_signInManager.IsSignedIn(User))
            {
                var UserId = _userManager.GetUserId(User);

                return cart = OrderHistory.GetCart(DbContext, HttpContext, UserId);
            }
            else
            {
                return null;
            }
        }

        public string CheckId()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return _userManager.GetUserId(User);
            }
            else
            {
                return null;
            }
        }
    }
}