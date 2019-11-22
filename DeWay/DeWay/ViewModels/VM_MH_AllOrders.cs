using DeWay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeWay.ViewModels
{
    public class VM_MH_AllOrders
    {
        public List<Order> Order { get; set; }
        public List<Cart_OrderDetail >OrderDetail { get; set; }
        public List<Product> Product { get; set; }
        public List<ProductImage> pdtImage { get; set; }
        public List<Review> Review { get; set; }
        public List<Specification> Specification { get; set; }
    }
}