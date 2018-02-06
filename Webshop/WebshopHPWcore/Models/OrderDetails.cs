using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebshopHPWcore.Models
{
    public partial class Orderdetail
    {
        [Key]
        public string Orderid { get; set; }

        [Required]
        public string Email { get; set; }
        public string Lastname { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Housenumber { get; set; }
        public string Firstname { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Userid { get; set; }


        public string Middlename { get; set; }
    }
}