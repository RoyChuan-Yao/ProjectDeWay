using DeWay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
        public ActionResult Edit(string pdtid)
        {


            Product product = (from m in db.Product
                               where m.pdtID == pdtid
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
            return RedirectToAction("myProduct", "SellerHome");
        }
        public ActionResult Editsp(string pdtid)
        {
            
            var spe = db.Specification.Where(m => m.pdtID == pdtid).OrderBy(m => m.spcID).ToList(); //將符合條件者全部取出
            return View(spe);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editsp(Specification sp)
        {
            var getroute = sp.pdtID;
            var spc = db.Specification.Where(m => m.spcID == sp.spcID).FirstOrDefault(); //只修改單筆
            spc.Style= sp.Style;
            spc.Size = sp.Size;
            spc.Stock = sp.Stock;
            spc.Price = sp.Price;
            spc.Discount = sp.Discount;
           
                db.SaveChanges();
                
            

            return RedirectToAction("Editsp", "Product_edit_Seller",new { pdtid = getroute }); //存檔後留在原頁面
        }
    }
}