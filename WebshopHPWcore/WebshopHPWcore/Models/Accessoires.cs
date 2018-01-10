using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebshopHPWcore.Models
{
    public class Accessoires
        {
        [Key]
        public int AccessoireId { get; set; }

        [Required]
        public string brand { get; set; }
        public string model { get; set; }
        public int Count { get; set; }
        public decimal price { get; set; }
    }

}
