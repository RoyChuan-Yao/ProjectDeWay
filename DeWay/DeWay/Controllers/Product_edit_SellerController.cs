using DeWay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DeWay.Controllers
{
    public class Product_edit_SellerController : Controller
    {
        shopDBEntities db = new shopDBEntities();
        // GET: Product_edit_Seller
        public ActionResult sellproduct(string id)
        {

           

            var selID = (from m in db.Seller
                         where m.mbrID == id
                         select m).FirstOrDefault().selID;

            var product = db.Product.Where(m => m.selID == selID).ToList();

            return View(product);


        }
        public ActionResult Edit()
        {
            
            var id = "pdt0000001";
            Product product = (from m in db.Product
                               where m.pdtID == id
                               select m).FirstOrDefault();

            
            return View(product);
            

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product p)
        {
            
            var product = db.Product.Where(m => m.pdtID == p.pdtID).FirstOrDefault();
            product.pdtName = p.pdtName;           
            product.pdtDescribe = p.pdtDescribe;
            product.Discontinued = p.Discontinued;                     
            db.SaveChanges();
            return View();


        }


    }
}