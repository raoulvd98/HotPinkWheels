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
    public class CarpageModel : PageModel
    {
        public List<Car> Cars { get; private set; } = new List<Car>();

        public void OnGet()
        {
            // using (var db = new ShopContext())
            // {
            //     foreach (var car in db.Cars)
            //     {
            //         Cars.Add(car);
            //     }
            // }
            // int i = 1;

            // ViewData["carheader"] = Cars.ElementAt(i).Brand + " " + Cars.ElementAt(i).Model;
            // ViewData["cardetails"] = "<li>Amount of doors: " + Cars.ElementAt(i).AmountofDoors + "</li>"
            //     + "<li>Enginetype: " + Cars.ElementAt(i).Enginetype + "</li>"
            //     + "<li>Fuel type: " + Cars.ElementAt(i).Fueltype + "</li>"
            //     + "<li>Manufacturing year: " + Cars.ElementAt(i).ManufactureYear + "</li>";
        }
    }
}
