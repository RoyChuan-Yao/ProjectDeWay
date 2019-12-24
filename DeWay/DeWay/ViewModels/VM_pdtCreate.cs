using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeWay.Models;

namespace DeWay.ViewModels
{
    public class VM_pdtCreate
    {
        public Product products { get; set; }
        public List<Specification> specification { get; set; }

        public List<FirstLayer> firstLayers { get; set; }

        public List<Tag> tag { get; set; }

        public List<ShipperDetail> shipperDetail { get; set; }


    }
}