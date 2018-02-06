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

    public class AccessoireController : Controller
    {
        private readonly ShopContext _context;

        public AccessoireController(ShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string currentFilter,
                                                 int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            IQueryable<string> genreQuery = from m in _context.cars
                                            orderby m.color
                                            select m.color;

            var cars = from s in _context.Accessoires
                       where s.Count > 0
                       select s;


            int pageSize = 15;

            return View(await PaginatedList<Accessoires>.CreateAsync(cars.AsNoTracking(), page ?? 1, pageSize));
        }
 
    }
}
