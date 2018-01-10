using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebsiteHotPinkWheels.Pages
{
    public class LoginModel : PageModel
    {
        public DateTime Message { get; set; }

        public void OnGet()
        {
            Message = DateTime.Now.AddYears(-18).Date;
        }
    }
}