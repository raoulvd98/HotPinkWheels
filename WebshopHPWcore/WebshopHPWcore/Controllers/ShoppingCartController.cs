using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebshopHPWcore.Models;
using WebshopHPWcore.Models.ShoppingCartViewModels;
using WebshopHPWcore.Models.HistoryViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Npgsql.EntityFrameworkCore;
using Npgsql;

namespace WebshopHPWcore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(ShopContext dbContext, ILogger<ShoppingCartController> logger,
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            DbContext = dbContext;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public ShopContext DbContext { get; }

        //
        // GET: /ShoppingCart/
        public async Task<IActionResult> Index()
        {
            ShoppingCart cart;
            cart = UserLogin();

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = await cart.GetCartItems(),
                CartTotal = await cart.GetTotal()
            };

            // Return the view
            return View(viewModel);
        }

        //
        // GET: /ShoppingCart/AddToCart/5
        public async Task<IActionResult> AddToCart(int id, CancellationToken requestAborted)
        {
            // Retrieve the album from the database
            ShoppingCart cart;
            string userid = CheckId();

            var CartItemCheck = DbContext.CartItems.Where(x => x.carid == id && x.userid == userid).ToList();
            var addedCar = await DbContext.cars.SingleAsync(car => car.carid == id);

            // Add it to the shopping cart
            cart = UserLogin();

            if (CartItemCheck.Count() == 0)
            {
                await cart.AddToCart(addedCar);
            }

            await DbContext.SaveChangesAsync(requestAborted);
            _logger.LogInformation("Auto {brand} is toegevoegd aan de winkelwagen", addedCar.brand);

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
            var cartItem = await DbContext.CartItems
                .Where(item => item.CartItemId == id)
                .Include(c => c.Car)
                .SingleOrDefaultAsync();

            string message;
            int itemCount;
            if (cartItem != null)
            {
                // Remove from cart
                itemCount = cart.RemoveFromCart(id);

                await DbContext.SaveChangesAsync(requestAborted);

                string removed = (itemCount > 0) ? " 1 product van " : string.Empty;
                message = removed + cartItem.Car.brand + " " + cartItem.Car.model + " is uit uw winkelwagen verwijderd.";
            }
            else
            {
                itemCount = 0;
                message = "Kon product niet vinden, er is niks uit u winkelwagen verwijderd.";
            }

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = message,
                CartTotal = await cart.GetTotal(),
                CartCount = await cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            _logger.LogInformation("Auto {id} is uit een winkelwagen verwijderd.", id);

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

        public ShoppingCart UserLogin()
        {
            ShoppingCart cart;

            if (_signInManager.IsSignedIn(User))
            {
                var UserId = _userManager.GetUserId(User);

                return cart = ShoppingCart.GetCart(DbContext, HttpContext, UserId);
            }
            else
            {
                return cart = ShoppingCart.GetCart(DbContext, HttpContext);
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
                return "";
            }
        }

        public static string GetOrderId()
        {
            string CartItemId;

            //A GUID to hold the CartItemId. 
            CartItemId = Guid.NewGuid().ToString();

            return CartItemId;
        }
        ReceiptViewModel receiptView = new ReceiptViewModel();
        public async Task<IActionResult> Review()
        {
            ShoppingCart cart;
            cart = UserLogin();

            // Set up our ViewModel
            var ShoppingcartViewModel = new ShoppingCartViewModel
            {
                CartItems = await cart.GetCartItems(),
                CartTotal = await cart.GetTotal()
            };

            var user = await _userManager.GetUserAsync(User);

            receiptView.shoppingcart = ShoppingcartViewModel;

            if (_signInManager.IsSignedIn(User))
            {
                var usert = new ApplicationUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    HouseNumber = user.HouseNumber,
                    ZipCode = user.ZipCode,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    lastName = user.lastName,
                    City = user.City
                };
                receiptView.user = usert;
            }
            else
            {
                receiptView.user = new ApplicationUser { };
            }

            // Return the view
            return View(receiptView);
        }

        public async Task<IActionResult> Finish()
        {
            return RedirectToAction("Cancellation", "ShoppingCart");
        }

        [HttpPost("/ShoppingCart/Finish")]
        public async Task<IActionResult> Finish(string Email, string Firstname,
                                    string Middlename, string Lastname, string Addres,
                                    string Housenr, string Zipcode, string City, string Phonenr, string Toevoeging)
        {
            string id = GetOrderId();
            string Userid = CheckId();
            if(Userid == "" || String.IsNullOrEmpty(Userid))
            {
                Userid = "Not Reg " + Guid.NewGuid().ToString(); ;
            }

            ShoppingCart cart = UserLogin();
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = await cart.GetCartItems()
            };
            ViewBag.BovensteAuto = viewModel.CartItems[0].Car.color;
            Orderdetail detail = new Orderdetail
            {
                Orderid = id,
                Email = Email,
                Lastname = Lastname,
                Phonenumber = Phonenr,
                Address = Addres,
                Housenumber = Housenr + Toevoeging,
                Firstname = Firstname,
                Zipcode = Zipcode,
                City = City,
                Middlename = Middlename,
                Userid = Userid
            };
            DbContext.orderdetail.Add(detail);
            DbContext.SaveChanges();

            await cart.AddToHistory(viewModel.CartItems, id, Userid);

            foreach (var item in viewModel.CartItems)
            {
                cart.RemoveFromCart(item.CartItemId);
            }
            await cart.CountDecrease(viewModel.CartItems);

            return View();
        }

        public async Task<IActionResult> Cancellation()
        {
            return View();
        }

        public async Task<IActionResult> Checkout(string Username, string Email, string Firstname,
                                    string Middlename, string Lastname, string Addres,
                                    string Housenr, string Zipcode, string City, string Phonenr, string Toevoeging)
        {
            ShoppingCart cart;
            cart = UserLogin();

            // Set up our ViewModel
            var ShoppingcartViewModel = new ShoppingCartViewModel
            {
                CartItems = await cart.GetCartItems(),
                CartTotal = await cart.GetTotal()
            };

            var user = await _userManager.GetUserAsync(User);

            receiptView.shoppingcart = ShoppingcartViewModel;

            var usert = new ApplicationUser
            {
                UserName = Username,
                Email = Email,
                PhoneNumber = Phonenr,
                Address = Addres,
                HouseNumber = Housenr,
                ZipCode = Zipcode,
                FirstName = Firstname,
                MiddleName = Middlename,
                lastName = Lastname,
                City = City
            };
            receiptView.user = usert;

            return View(receiptView);
        }
    }
}
