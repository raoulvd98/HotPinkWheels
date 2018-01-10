using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebshopHPW.Models;

using System.IO;

namespace WebshopHPW.Controllers
{
    public class CarController : Controller
    {
        private readonly ShopContext _context;

        public CarController(ShopContext context)
        {
            _context = context;
        }

        // GET: Car
        public async Task<IActionResult> Index(string searchString, string currentFilter, 
                                                 string carColor, string currentColorFilter, 
                                                 string fueltype, string fueltypeFilter,
                                                 string motortype, string motortypeFilter,
                                                 string transmission, string transmissionFilter,
                                                 int? page)
        {
            if (searchString != null || carColor != null || fueltype != null || motortype != null || transmission != null )
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
                carColor = currentColorFilter;
                fueltype = fueltypeFilter;
                motortype = motortypeFilter;  
                transmission = transmissionFilter;
                //milage = milageFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["ColorFilter"] = carColor;
            ViewData["FuelTypeFilter"] = fueltype;
            ViewData["MotorTypeFilter"] = motortype;
            ViewData["TransmissionFilter"] = transmission;
            //ViewData["MilageFilter"] = milage;

            IQueryable<string> genreQuery = from m in _context.cars
                                            orderby m.color
                                            select m.color;

            var cars = from s in _context.cars
                           select s;

            if(!String.IsNullOrEmpty(searchString))
            {
               cars = cars.Where(m => m.brand.Contains(searchString) || m.model.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(carColor))
            {
                cars = cars.Where(x => x.color.Contains(carColor));
            }

            if (!String.IsNullOrEmpty(fueltype))
            {
                cars = cars.Where(x => x.fueltype.Contains(fueltype));
            }

            if (!String.IsNullOrEmpty(motortype))
            {
                cars = cars.Where(x => x.enginetype.Contains(motortype));
            }             
            if (!String.IsNullOrEmpty(transmission))
            {
                cars = cars.Where(x => x.transmission.Contains(transmission));
            }
            
            //if (milage != null)
            //{
            //    cars = cars.Where(x => (int)x.mileage > (int)milage);
            //}
            // var carColorVM = new Car();
            // carColorVM.colors = new SelectList(await genreQuery.Distinct().ToListAsync());


            int pageSize = 15;

            return View(await PaginatedList<Car>.CreateAsync(cars.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.cars
                .SingleOrDefaultAsync(m => m.carid == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("carid,brand,model,manufactureyear,color,price,horsepower,weight,topspeed,enginetype,fueltype,fuelusage,transmission,mileage,apk,warranty,amountofdoors,amountofseats,amountofpreviousowners,image")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.cars.SingleOrDefaultAsync(m => m.carid == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("carid,brand,model,manufactureyear,color,price,horsepower,weight,topspeed,enginetype,fueltype,fuelusage,transmission,mileage,apk,warranty,amountofdoors,amountofseats,amountofpreviousowners,image")] Car car)
        {
            if (id != car.carid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.carid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.cars
                .SingleOrDefaultAsync(m => m.carid == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.cars.SingleOrDefaultAsync(m => m.carid == id);
            _context.cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.cars.Any(e => e.carid == id);
        }

        public async Task<IActionResult> Carpage(string searchString, string currentFilter, 
                                                 string carColor, string currentColorFilter, 
                                                 string fueltype, string fueltypeFilter,
                                                 string motortype, string motortypeFilter,
                                                 string transmission, string transmissionFilter,
                                                 int? page)
        {
            if (searchString != null || carColor != null || fueltype != null || motortype != null || transmission != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
                carColor = currentColorFilter;
                fueltype = fueltypeFilter;
                motortype = motortypeFilter;  
                transmission = transmissionFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["ColorFilter"] = carColor;
            ViewData["FuelTypeFilter"] = fueltype;
            ViewData["MotorTypeFilter"] = motortype;
            ViewData["TransmissionFilter"] = transmission;

            IQueryable<string> genreQuery = from m in _context.cars
                                            orderby m.color
                                            select m.color;

            var cars = from s in _context.cars
                           select s;

            if(!String.IsNullOrEmpty(searchString))
            {
               cars = cars.Where(m => m.brand.Contains(searchString) || m.model.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(carColor))
            {
                cars = cars.Where(x => x.color.Contains(carColor));
            }

            if (!String.IsNullOrEmpty(fueltype))
            {
                cars = cars.Where(x => x.fueltype.Contains(fueltype));
            }

            if (!String.IsNullOrEmpty(motortype))
            {
                cars = cars.Where(x => x.enginetype.Contains(motortype));
            }             
            if (!String.IsNullOrEmpty(transmission))
            {
                cars = cars.Where(x => x.transmission.Contains(transmission));
            }             
            // var carColorVM = new Car();
            // carColorVM.colors = new SelectList(await genreQuery.Distinct().ToListAsync());


            int pageSize = 15;

            return View(await PaginatedList<Car>.CreateAsync(cars.AsNoTracking(), page ?? 1, pageSize));
        }

        //[HttpGet ("Car/Carpage/{page_index}/{page_size}")]
        //public IActionResult GetCarsPaged(int page_index, int page_size)
        //{
        //    var res = _context.cars.GetPage<Car>(page_index, page_size, a => a.carid);
        //    if(res == null) return NotFound();
        //    return Ok(res);
        //}   
    }
}
