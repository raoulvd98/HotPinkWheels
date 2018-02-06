using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopHPWcore.Models
{
    public class TopCarsViewModel
    {
        public List<TopCarItem> cars { get; set; }   
        
    }

    public class TopCarItem
    {
        public Car Car;
        public int CarCount;
        public TopCarItem(Car car, int count)
        {
            this.Car = car;
            this.CarCount = count;
        }
    }
}
