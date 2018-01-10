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
    public class ShopContext : DbContext
    {
        public DbSet<Car> Cars {get; set;}

        //this method is run automatically by EF the first time we run the application
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //here we define the name of our database
            optionsBuilder.UseNpgsql("User ID=postgres;Password=hallo;Host=localhost;Port=5432;Database=CarSalesDb;Pooling=true;");
        }
    }
}