using DeWay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeWay.ViewModels
{
    public class VM_rfdDetail
    {
        public List< Refund> refund { get; set; }
        public List< Cart_OrderDetail> cart_orderDetail { get; set; }

       
    }
}