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

namespace WebsiteHotPinkWheels.Models
{
    public class User
    {
        public int UserID {get; set;}
        public string ZipCode {get; set;}
        public string Address {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Mail {get; set;}
        public string PassWord {get; set;}
        public string PhoneNumber {get; set;}
        public DateTime DateOfBirth {get; set;}
    }
}