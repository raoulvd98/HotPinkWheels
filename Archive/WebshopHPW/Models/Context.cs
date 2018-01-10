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

namespace WebshopHPW.Models
{
    public class ShopContext : DbContext
    {
        public DbSet<Car> cars {get; set;}
        public DbSet<User> Users { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options): base(options)
        {
            
        }
    }
}