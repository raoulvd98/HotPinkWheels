using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebsiteHotPinkWheels.Models;

namespace WebsiteHotPinkWheels
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

            //using(var db = new ShopContext())
            //{
            //    Car car = new Car
            //    {
            //        CarID = 2,
            //        AmountofDoors = 60,
            //        AmountofPreviousOwners = 6,
            //        AmountofSeats = 1,
            //        APK = new DateTime(1997, 12, 2),
            //        Brand = "Susuki",
            //        Color = "Blue",
            //        Enginetype = "V6",
            //        Fueltype = "Diesel",
            //        Fuelusage = 8,
            //        Horsepower = 350,
            //        Image = "auto2.jpg",
            //        ManufactureYear = 1994,
            //        Mileage = 200000,
            //        Model = "Swift",
            //        Price = 16001,
            //        Topspeed = 400,
            //        Transmission = "nog steeds geen idee van wat dat is",
            //        Warranty = new DateTime(1699, 12, 12),
            //        Weight = 200
            //    };

            //    db.Cars.Add(car);
            //    db.SaveChanges();
            //}
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
