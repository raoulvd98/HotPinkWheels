using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopHPWcore.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Het {0} moet op zijn minst {2} en maximaal {1} tekens lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("Password", ErrorMessage = "De wachtwoorden komen niet overeen.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Telefoonnummer")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name ="Postcode")]
        public string ZipCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Woonplaats")]
        public string City { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Voornaam")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Straatnaam")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Huisnummer")]
        public string HouseNumber { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Tussenvoegsel")]
        public string MiddleName { get; set; }
    }
}
