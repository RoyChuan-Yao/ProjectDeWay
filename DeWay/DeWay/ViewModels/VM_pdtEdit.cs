using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeWay.Models;

namespace DeWay.ViewModels
{
    public class VM_pdtEdit
    {
        public Product products { get; set; }
        public IEnumerable<Specification> specifications { get; set; }




    }
}