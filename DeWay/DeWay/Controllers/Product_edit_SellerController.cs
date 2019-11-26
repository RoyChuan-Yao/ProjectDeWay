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

            //string id = Session["memberID"].ToString();

            var selID = (from m in db.Seller
                         where m.mbrID == id
                         select m).FirstOrDefault().selID;

            var product = db.Product.Where(m => m.selID == selID).ToList();

            return View(product);


        }
        public ActionResult Edit()
        {
            //if (Session["memberID"] != null)
            //{
            var id = "pdt0000001";
            Product product = (from m in db.Product
                               where m.pdtID == id
                               select m).FirstOrDefault();

            //ViewBag.ctgID = new SelectList(db.Product, "ctgID");
            return View(product);
            //}

            //return RedirectToAction("sellproduct");

        }
        [HttpPost]
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