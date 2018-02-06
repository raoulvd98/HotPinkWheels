using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebshopHPWcore.Models;
using Microsoft.EntityFrameworkCore;


namespace WebshopHPWcore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopContext _context;

        public HomeController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new TopCarsViewModel
            {
                cars = GetTop5HistoryItems()
            };
           return View(model);
        }

        public List<TopCarItem> GetTop5HistoryItems()
        {
            List<TopCarItem> dataPoints = new List<TopCarItem>();

            var cars = _context.cars.Where(x => x.Count == 0).Select(x => x).Distinct().ToList();
            foreach (var item in cars)
            {
                Car car = item;
                int Count = 0;

                foreach (var test in _context.cars.Where(x => x.model == item.model).Where(x => x.Count == 0).Select(x => x))
                {
                    Count += 1;
                }

                TopCarItem z = new TopCarItem(car, Count);
                dataPoints.Add(z);

            }
            return dataPoints;
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }
        
        public Dictionary<Car, Int32> GetAllBestCars()
        {
            var cars = _context.cars.Where(x => x.Count == 0);
            Dictionary<Car, Int32> CarsList = new Dictionary<Car, Int32>();

            foreach (var item in cars)
            {
                string model = item.brand + " " + item.model;
                int Count = 0;

                foreach (var test in _context.cars.Where(x => x.model == item.model).Where(x => x.Count == 0).Select(x => x))
                {
                    Count += 1;
                }

                CarsList.Add(item, Count);
            }

            CarsList.OrderBy(key => key.Value);

            return CarsList;

            //return _context
            //       .cars
            //       .Where(x => x.Count == 0)
            //       .ToListAsync();
        }
    }
}
