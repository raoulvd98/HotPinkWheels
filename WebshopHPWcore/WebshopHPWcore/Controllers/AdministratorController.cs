using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Text.Encodings.Web;
using System.IO;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using WebshopHPWcore.Models.ManageViewModels;
using WebshopHPWcore.Services;
using WebshopHPWcore.Models;
using WebshopHPWcore.Controllers;

namespace WebshopHPWcore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {
        private readonly ShopContext _context;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministratorController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet("/userspage")]
        public async Task<IActionResult> ShowUsers(int? page)
        {
            var users = from s in _context.Users
                       select s;

            //var userrole = from s in _context.Roles
            //               join usertorole in _context.UserRoles on s.Id equals usertorole.RoleId
            //               join user in _context.Users on usertorole.UserId equals user.Id
            //               select new
            //               {
            //                   s.Name
            //               };
            //join mi in _context.UserRoles on s.Id equals mi.UserId
            //join li in _context.Roles on mi.RoleId equals li.Id
            //select new
            //{

            //};

            //ViewData["userroles"] = userrole.ToString();

            int pageSize = 15;
            return View(await PaginatedList<ApplicationUser>.CreateAsync(users.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Administrator
        [HttpGet("/adminpage")]
        public async Task<IActionResult> Index(string searchString, string currentFilter,
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

            if (!String.IsNullOrEmpty(searchString))
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

        // GET: Administrator/Details/5
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

       
        public async Task<IActionResult> UserDetails(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Administrator/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Create
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

        // GET: Administrator/Edit/5
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

        // POST: Administrator/Edit/5
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

        // GET: Administrator/Delete/5
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

        // POST: Administrator/Delete/5
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
