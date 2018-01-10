using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteHotPinkWheels.Models;

namespace WebsiteHotPinkWheels.Controllers
{
    public class CarController : Controller
    {
        private readonly ShopContext _context;

        public CarController(ShopContext context)
        {
            _context = context;
        }

        // GET: Car
        public async Task<IActionResult> Index(string searchString)
        {
            var movies = _context.cars.Select(m => m);

            if(!String.IsNullOrEmpty(searchString))
            {
                movies = _context.cars.Where(m => m.brand.Contains(searchString));
            }

            return View(await movies.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("CarID,Brand,Model,ManufactureYear,Color,Price,Horsepower,Weight,Topspeed,Enginetype,Fueltype,Fuelusage,Transmission,Mileage,APK,Warranty,AmountofDoors,AmountofSeats,AmountofPreviousOwners")] Car car)
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
        public async Task<IActionResult> Edit(int id, [Bind("CarID,Brand,Model,ManufactureYear,Color,Price,Horsepower,Weight,Topspeed,Enginetype,Fueltype,Fuelusage,Transmission,Mileage,APK,Warranty,AmountofDoors,AmountofSeats,AmountofPreviousOwners")] Car car)
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
    }
}
