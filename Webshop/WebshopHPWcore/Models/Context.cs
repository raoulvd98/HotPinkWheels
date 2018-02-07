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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebshopHPWcore.Models;

namespace WebshopHPWcore.Models
{
    public class ShopContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Car> cars { get; set; }
        public DbSet<WebshopHPWcore.Models.ApplicationUser> ApplicationUser { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishListItem> WishListItems { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Orderdetail> orderdetail { get; set; }
        public DbSet<Accessoires> Accessoires { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options): base(options)
        {
            
        }   
    }
}