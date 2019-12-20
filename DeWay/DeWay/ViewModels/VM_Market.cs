using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeWay.Models;

namespace DeWay.ViewModels
{
    public class VM_Market
    {
        public List<Seller> sellers { get; set; }
        public List<Product> products { get; set; }
        public List<Review> reviews { get; set; }

    }
}