using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebshopHPWcore.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string HouseNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string lastName { get; set; }
        public string ZipCode { get; set; } 
        public string City { get; set; }
    }
}
