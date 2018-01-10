using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebshopHPWcore.Models
{
    public class Car
    {
        public int carid {get; set;}
        public string brand {get; set;}
        public string model {get; set;}
        public string manufactureyear {get; set;}
        public string color {get; set;}
        public decimal price {get; set;}

        public int horsepower {get; set;}
        public int weight {get; set;}
        public int topspeed {get; set;}

        public string enginetype {get; set;}
        public string fueltype {get; set;}
        public int fuelusage {get; set;}
        public string transmission {get; set;}

        public int mileage {get; set;}
        public DateTime apk {get; set;}
        public DateTime warranty {get; set;}

        public int amountofdoors {get; set;}
        public int amountofseats {get; set;}
        public int amountofpreviousowners {get; set;}

        public string image {get; set;}
        public int Count { get; set; }

    }

}


