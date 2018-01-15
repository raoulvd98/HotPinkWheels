using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebshopHPWcore.Models;
using System.IO;
using WebshopHPWcore.Controllers;

namespace WebshopHPWcore.Controllers
{

    public class CarController : Controller
    {
        private readonly ShopContext _context;

        public CarController(ShopContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Carpage(string searchString, string currentFilter,
                                                 string searchStringModel, string currentFilterModel,
                                                 string carColor, string currentColorFilter,
                                                 string fueltype, string fueltypeFilter,
                                                 string motortype, string motortypeFilter,
                                                 string transmission, string transmissionFilter,
                                                 int minPrice, int minPriceFilter,
                                                 int maxPrice, int maxPriceFilter,
                                                 int amountSeats, int amountSeatsFilter,
                                                 int usage, int usageFilter,
                                                 int pk, int pkFilter,
                                                 int milage, int milageFilter,
                                                 int topSpeed, int topSpeedFilter,
                                                 int minWeight, int minWeightFilter,
                                                 int maxWeight, int maxWeightFilter,
                                                 int? page)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilterModel"] = searchStringModel;
            ViewData["ColorFilter"] = carColor;
            ViewData["FuelTypeFilter"] = fueltype;
            ViewData["MotorTypeFilter"] = motortype;
            ViewData["TransmissionFilter"] = transmission;
            ViewData["MinPriceFilter"] = minPrice;
            ViewData["MaxPriceFilter"] = maxPrice;
            ViewData["AmountSeatsFilter"] = amountSeats;
            ViewData["UsageFilter"] = usage;
            ViewData["PkFilter"] = pk;
            ViewData["MilageFilter"] = milage;
            ViewData["TopSpeedFilter"] = topSpeed;
            ViewData["MinWeightFilter"] = minWeight;
            ViewData["MaxWeightFilter"] = maxWeight;

            var cars = from s in _context.cars
                       where s.Count > 0
                       select s;

            if (!String.IsNullOrEmpty(searchString)) { searchString = searchString.ToUpper();
                            cars = cars.Where(m =>  (m.brand).ToUpper().Contains(searchString)); }

            if (!String.IsNullOrEmpty(searchStringModel)) { searchStringModel = searchStringModel.ToUpper();
                            cars = cars.Where(m => (m.model).ToUpper().Contains(searchStringModel));  }

            if (!String.IsNullOrEmpty(carColor)) { carColor = carColor.ToUpper();
                            cars = cars.Where(x => (x.color).ToUpper().Contains(carColor)); }

            if (!String.IsNullOrEmpty(fueltype)) { fueltype = fueltype.ToUpper();
                            cars = cars.Where(x => (x.fueltype).ToUpper().Contains(fueltype)); }

            if (!String.IsNullOrEmpty(motortype)) { motortype = motortype.ToUpper();
                            cars = cars.Where(x => (x.enginetype).ToUpper().Contains(motortype)); }
                
            if (!String.IsNullOrEmpty(transmission)) { transmission = transmission.ToUpper();
                            cars = cars.Where(x => (x.transmission).ToUpper().Contains(transmission)); }

            if (maxPrice > 0) { cars = cars.Where(x => x.price <= maxPrice); }
            else { ViewData["MaxPriceFilter"] = cars.Select(x => x.price).Max(); }

            if (minPrice > 0) { cars = cars.Where(x => x.price >= minPrice); }
            else { ViewData["MinPriceFilter"] = cars.Select(x => x.price).Min(); }

            if (amountSeats > 0) { cars = cars.Where(x => x.amountofseats == amountSeats); }
            else { ViewData["AmountSeatsFilter"] = ""; }

            if (usage > 0) { cars = cars.Where(x => x.fuelusage <= usage); }
            else { ViewData["UsageFilter"] = ""; }

            if (pk > 0) { cars = cars.Where(x => x.horsepower >= pk); }
            else { ViewData["PkFilter"] = ""; }

            if (milage > 0) { cars = cars.Where(x => x.mileage <= milage); }
            else { ViewData["MilageFilter"] = "";  }

            if (topSpeed > 0) { cars = cars.Where(x => x.topspeed >= topSpeed); }
            else { ViewData["TopSpeedFilter"] = ""; }

            if (minWeight > 0) { cars = cars.Where(x => x.weight >= minWeight); }
            else { ViewData["MinWeightFilter"] = ""; }

            if (maxWeight > 0) { cars = cars.Where(x => x.weight <= maxWeight); }
            else { ViewData["MaxWeightFilter"] = ""; }

            int pageSize = 15;
            
            return View(await PaginatedList<Car>.CreateAsync(cars.AsNoTracking(), page ?? 1, pageSize));
        }  
    }
}
