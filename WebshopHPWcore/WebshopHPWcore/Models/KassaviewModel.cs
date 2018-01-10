using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopHPWcore.Models
{
    public class ReceiptViewModel
    {
        public Orderdetail orderdetail { get; set; }
        public ApplicationUser user { get; set; }
        public ShoppingCartViewModels.ShoppingCartViewModel shoppingcart { get; set; }
    }
}
