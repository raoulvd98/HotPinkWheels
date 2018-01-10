using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebsiteHotPinkWheels.Models;
using Microsoft.EntityFrameworkCore;

namespace WebsiteHotPinkWheels.Pages
{
    public class IndexModel : PageModel
    {
        

        public void OnGet()
        {
            //using (var db = new ShopContext())
            //{
            //    foreach (var car in db.Cars)
            //    {
            //        Cars.Add(car);
            //    }
            //}
            //ViewData["car1"] = Cars.ElementAt(1).Brand + Cars.ElementAt(1).Model;

            // ViewData["carheader"] = Cars.ElementAt(i).Brand + " " + Cars.ElementAt(i).Model;
            // ViewData["cardetails"] = "<li>Amount of doors: " + Cars.ElementAt(i).AmountofDoors + "</li>"
            //     + "<li>Enginetype: " + Cars.ElementAt(i).Enginetype + "</li>"
            //     + "<li>Fuel type: " + Cars.ElementAt(i).Fueltype + "</li>"
            //     + "<li>Manufacturing year: " + Cars.ElementAt(i).ManufactureYear + "</li>";

        }
    }  
}
