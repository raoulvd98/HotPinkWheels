using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebshopHPWcore.Models.HistoryViewModels
{
    public class HistoryViewModel : IdentityDbContext<ApplicationUser>
    {
        public List<History> HistoryItems { get; set; }

        public List<Orderdetail> orderdetail { get; set; }

        public decimal CartTotal { get; set; }
    }
}
