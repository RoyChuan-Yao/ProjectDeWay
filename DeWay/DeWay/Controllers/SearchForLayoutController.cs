using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;
using LinqKit;
using System.Collections;
using System.Windows.Documents;

namespace DeWay.Controllers
{
    public class SearchForLayoutController : Controller
    {
        public shopDBEntities db = new shopDBEntities();

       [HttpPost]
        public ActionResult Index( string ProductName = null,string fstID = null)
        {
            ViewBag.fstID = new SelectList(db.FirstLayer, "fstLayerID", "fstLayer");
            ViewBag.fst = db.FirstLayer.ToList();


            if (fstID != null && ProductName != null)
            {

                var product = db.Product.Where(m => m.ProductCategory.FirstLayer.fstLayerID.Contains(fstID));

                var result = product.Where(m => m.pdtName.Contains(ProductName)).Where(m => m.Discontinued == false);

                //var a = product.Where(m => m.pdtName.Contains(ProductName)).FirstOrDefault().ProductCategory.fstLayerID;

                //ViewBag.fst = db.FirstLayer.Where(m => m.fstLayerID == a).ToList();


                return View(result);

            }
            else if (!string.IsNullOrEmpty(ProductName))
            {
                var result = db.Product.Where(m => m.pdtName.Contains(ProductName)).Where(m => m.Discontinued == false);
                return View(result);
            }

            else if (!string.IsNullOrEmpty(fstID))
            {
                var product = db.Product.Where(m => m.ProductCategory.FirstLayer.fstLayerID.Contains(fstID)).Where(m=>m.Discontinued == false);
                return View(product);
            }
            
            else
            {
                var result = from m in db.Product
                             where m.Discontinued == false
                             select m;
                return View(result);
            }         
        }

        public ActionResult GetCategory(string category)
        {
            ViewBag.fst = db.FirstLayer.ToList();

            var show = db.Product.Where(m => m.ProductCategory.fstLayerID == category);
            
            return View("Index",show);
        }
    }
}