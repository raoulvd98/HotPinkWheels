using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopHPWcore.Models.WishListViewModels
{
    public class WishListRemoveViewModel
    {
        public string Message { get; set; }
        public int ItemCount { get; set; }
        public string DeleteId { get; set; }
    }
}
