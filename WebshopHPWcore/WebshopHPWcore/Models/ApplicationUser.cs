using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebshopHPWcore.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Straatnaam")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Huisnummer")]
        public string HouseNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Tussenvoegsel")]
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Achternaam")]
        public string lastName { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postcode")]
        public string ZipCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Woonplaats")]
        public string City { get; set; }
    }
}
