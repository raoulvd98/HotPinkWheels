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
using Microsoft.EntityFrameworkCore;

namespace HotPinkWheels.Models
{
    public class Car
    {
        public int CarID {get; set;}
        public string Brand {get; set;}
        public string Model {get; set;}
        public int ManufactureYear {get; set;}
        public string Color {get; set;}
        public int Price {get; set;}

        public int Horsepower {get; set;}
        public int Weight {get; set;}
        public int Topspeed {get; set;}

        public string Enginetype {get; set;}
        public string Fueltype {get; set;}
        public int Fuelusage {get; set;}
        public string Transmission {get; set;}

        public int Mileage {get; set;}
        public DateTime APK {get; set;}
        public DateTime Warranty {get; set;}

        public int AmountofDoors {get; set;}
        public int AmountofSeats {get; set;}
        public int AmountofPreviousOwners {get; set;}

        public string Image {get; set;}

    }
}