using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebshopHPWcore.Models;
using WebshopHPWcore.Models.WishListViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebshopHPWcore.Controllers
{
    [Authorize]

    public class WishListController : Controller
    {
        private readonly ILogger<WishListController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public WishListController(ShopContext dbContext, ILogger<WishListController> logger,
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            DbContext = dbContext;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public ShopContext DbContext { get; }

        //
        // GET: /WishList/
        public async Task<IActionResult> Index()
        {
            WishListFuncties cart;
            cart = UserLogin();

            // Set up our ViewModel
            var viewModel = new WishListViewModel
            {
                WishListItems = await cart.GetWishListItems()
            };

            // Return the view
            return View(viewModel);
        }

        //
        // GET: /WishList/AddToCart/5
        public async Task<IActionResult> AddToCart(int id, CancellationToken requestAborted)
        {
            // Retrieve the album from the database
            WishListFuncties cart;

            string userid = CheckId();

            var WishListCheck = DbContext.WishListItems.Where(x => x.carid == id && x.userid == userid).ToList();
            
            var addedCar = await DbContext.cars
                .SingleAsync(car => car.carid == id);

            // Add it to the shopping cart
            cart = UserLogin();
            
            if (WishListCheck.Count() == 0)
            {
                await cart.AddToWishList(addedCar);
            }

            await DbContext.SaveChangesAsync(requestAborted);
            _logger.LogInformation("Auto {brand} is toegevoegd aan uw wenslijstje", addedCar.brand);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(string id, CancellationToken requestAborted)
        {
            // Retrieve the current user's shopping cart
            var cart = UserLogin();

            // Get the name of the album to display confirmation
            var cartItem = await DbContext.WishListItems
                .Where(item => item.WishListItemId == id)
                .Include(c => c.Car)
                .SingleOrDefaultAsync();

            string message;
            int itemCount;
            if (cartItem != null)
            {
                // Remove from cart
                itemCount = cart.RemoveFromWishList(id);

                await DbContext.SaveChangesAsync(requestAborted);

                string removed = (itemCount > 0) ? " 1 product van " : string.Empty;
                message = removed + cartItem.Car.brand + " " + cartItem.Car.model + " is uit uw wenslijst verwijderd.";
            }
            else
            {
                itemCount = 0;
                message = "Kon product niet vinden, er is niks uit u wenslijst verwijderd.";
            }

            // Display the confirmation message
            var results = new WishListRemoveViewModel
            {
                Message = message,
                ItemCount = itemCount,
                DeleteId = id
            };

            _logger.LogInformation("Auto {id} is uit een wenslijst verwijderd.", id);

            return Json(results);
        }
        // GET: Car/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await DbContext.cars
                .SingleOrDefaultAsync(m => m.carid == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        public WishListFuncties UserLogin()
        {
            WishListFuncties cart;

            if (_signInManager.IsSignedIn(User))
            {
                var UserId = _userManager.GetUserId(User);

                return cart = WishListFuncties.GetCart(DbContext, HttpContext, UserId);
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
