using DeWay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeWay.ViewModels
{
    public class VM_MH_AllOrders
    {
        public Order Order { get; set; }
        public Cart_OrderDetail OrderDetail { get; set; }
        public Product Product { get; set; }
        public ProductImage pdtImage { get; set; }
        public Review Review { get; set; }
    }
}