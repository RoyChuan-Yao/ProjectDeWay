using DeWay.Models;
using DeWay.ViewModels;
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
        public ActionResult Edit(string pdtid)
        {

            VM_pdtEdit vm = new VM_pdtEdit();
            var spcID = db.Specification.Where(m => m.pdtID == pdtid).FirstOrDefault().spcID;
            
            Product product = (from m in db.Product
                               where m.pdtID == pdtid
                               select m).FirstOrDefault(); //抓取商品資料
            //Specification spe = new Specification();

           var spe = db.Specification.Where(m => m.pdtID == pdtid).OrderBy(m => m.pdtID == pdtid).ToList();
                                //抓取商品規格資料
            vm.products = product;
            vm.specifications = spe;


            return View(vm);
            

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product p, Specification sp)
        {
            

            var product = db.Product.Where(m => m.pdtID == p.pdtID).FirstOrDefault();
            product.pdtName = p.pdtName;           
            product.pdtDescribe = p.pdtDescribe;
            product.Discontinued = p.Discontinued;
            var spefication = db.Specification.Where(m => m.pdtID == sp.pdtID).FirstOrDefault();
            spefication.Style = sp.Style;
            spefication.Size = sp.Size;
            spefication.Stock = sp.Stock;
            spefication.Price = sp.Price;
            spefication.Discount = sp.Discount;
            db.SaveChanges();
            return View();


        }


    }
}