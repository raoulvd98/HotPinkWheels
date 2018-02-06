using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebshopHPWcore.Models
{
    public class WishListItem
    {
        [Key]
        public string WishListItemId { get; set; }

        [Required]
        public int carid { get; set; }
        public int Count { get; set; }
        public string userid { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        public virtual Car Car { get; set; }
    }
}

