//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace WebshopHPWcore.Models
//{
//    public partial class Order
//    {
//        public int OrderId { get; set; }
//        public string Username { get; set; }
//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public string Address { get; set; }
//        public string City { get; set; }
//        public string PostalCode { get; set; }
//        public string Country { get; set; }
//        public string Phone { get; set; }
//        public string Email { get; set; }
//        public decimal Total { get; set; }
//        public System.DateTime OrderDate { get; set; }
//        public List<OrderDetail> OrderDetails { get; set; }
//    }

//    public class OrderDetail
//    {
//        public int OrderDetailId { get; set; }
//        public int OrderId { get; set; }
//        public int carid { get; set; }
//        public int Quantity { get; set; }
//        public decimal UnitPrice { get; set; }
//        public virtual Car Car { get; set; }
//        public virtual Order Order { get; set; }
//    }
//}
