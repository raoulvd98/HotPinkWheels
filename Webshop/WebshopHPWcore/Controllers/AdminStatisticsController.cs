using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebshopHPWcore.Models.HistoryViewModels;
using WebshopHPWcore.Models.ManageViewModels;
using WebshopHPWcore.Services;
using WebshopHPWcore.Models;
using WebshopHPWcore.Controllers;

namespace WebshopHPWcore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminStatisticsController : Controller
    {
        private readonly ILogger<HistoryController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ShopContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        
        public AdminStatisticsController(ShopContext context, ILogger<HistoryController> logger,
        SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Omzet()
        {
            List<DataPoint2> dataPoints3 = RevenueDay();
            ViewBag.dataPoints3 = JsonConvert.SerializeObject(dataPoints3);

            List<DataPoint2> dataPoints4 = RevenueMonth();
            ViewBag.dataPoints4 = JsonConvert.SerializeObject(dataPoints4);

            List<DataPoint2> dataPoints5 = RevenueYear();
            ViewBag.dataPoints5 = JsonConvert.SerializeObject(dataPoints5);

            return View();
        }

        public IActionResult TopTen()
        {
            List<DataPoint> dataPoints2 = HistoryModelStatistics();
            List<DataPoint> dataPoints = HistoryBrandStatistics();

            ViewBag.dataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.dataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            
            return View();
        }

        public async Task<IActionResult> TopTenUsers()
        {
            var viewModel = new HistoryViewModel
            {
                HistoryItems = await GetAllHistoryItems(),
                orderdetail = await GetAllOrderDetails()
            };
            return View(viewModel); 
        }

        public OrderHistory UserLogin()
        {
            OrderHistory cart;

            if (_signInManager.IsSignedIn(User))
            {
                var UserId = _userManager.GetUserId(User);

                return cart = OrderHistory.GetCart(_context, HttpContext, UserId);
            }
            else
            {
                return null;
            }
        }
        
        public List<DataPoint> HistoryBrandStatistics()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();

            var cars = _context.cars.Where(x => x.Count == 0).Select(x => x.brand).Distinct().ToList();
            foreach (var item in cars)
            {
                string brand = item;
                int Count = 0;

                foreach (var test in _context.cars.Where(x => x.brand == item).Where(x => x.Count == 0).Select(x => x))
                {
                    Count += 1;
                }

                DataPoint z = new DataPoint(brand, Count);
                dataPoints.Add(z);
                
            }
            return dataPoints;
        }

        public Task<List<History>> GetAllHistoryItems()
        {
            return  _context
                   .History
                   .Include(c => c.Car)
                   .ToListAsync();
        }

        public Task<List<Orderdetail>> GetAllOrderDetails()
        {
            return _context
                   .orderdetail
                   .ToListAsync();
        }

        public List<DataPoint> HistoryModelStatistics()
        {
            List<DataPoint> dataPoints2 = new List<DataPoint>();

            var cars = _context.cars.Where(x => x.Count == 0).Select(x => new { x.model, x.brand }).Distinct().ToList();
            foreach (var item in cars)
            {
                string model = item.brand + " " + item.model;
                int Count = 0;

                foreach (var test in _context.cars.Where(x => x.model == item.model).Where(x => x.Count == 0).Select(x => x))
                {
                    Count += 1;
                }

                DataPoint z = new DataPoint(model, Count);
                dataPoints2.Add(z);
            }
            return dataPoints2;
        }

        public List<DataPoint2> RevenueDay()
        {
            List<DataPoint2> dataPoints3 = new List<DataPoint2>();

            for (int DayCounter = 0; DayCounter > -1000; DayCounter--)
            {
                var price = _context.History.Where(x => x.DateCreated.ToShortDateString() == DateTime.Now.AddDays(DayCounter).ToShortDateString()).Sum(y => y.Price);
                
                DataPoint2 z = new DataPoint2(price, DateTime.Now.AddDays(DayCounter).ToShortDateString());
                dataPoints3.Add(z);
            }
         
            return dataPoints3;
        }

        public List<DataPoint2> RevenueMonth()
        {
            List<DataPoint2> dataPoints4 = new List<DataPoint2>();
            for (int MonthCounter = 0; MonthCounter > -1000; MonthCounter--)
            {
                decimal price = 0;
                DateTime time = DateTime.Now.AddMonths(MonthCounter);
                foreach (var item in _context.History.Where(x => x.DateCreated.Month == time.Month && x.DateCreated.Year == time.Year).Select(x => x.Price))
                {
                    price += item;
                }
                DataPoint2 z = new DataPoint2(price, time.Month.ToString() + "-" + time.Year.ToString());
                dataPoints4.Add(z);
            }

            return dataPoints4;
        }

        public List<DataPoint2> RevenueYear()
        {
            List<DataPoint2> dataPoints5 = new List<DataPoint2>();
            for (int YearCounter = 0; YearCounter > -100; YearCounter--)
            {
                decimal price = 0;
                DateTime time = DateTime.Now.AddYears(YearCounter);
                foreach (var item in _context.History.Where(x => x.DateCreated.Year == time.Year).Select(x => x.Price))
                {
                    price += item;
                }
                DataPoint2 z = new DataPoint2(price, time.Year.ToString());
                dataPoints5.Add(z);
            }
            
            return dataPoints5;
        }
    }
}