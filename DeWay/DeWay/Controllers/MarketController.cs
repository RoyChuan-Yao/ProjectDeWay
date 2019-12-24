using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.ViewModels;
using DeWay.Models;

namespace DeWay.Controllers
{
    public class MarketController : Controller
    {
        shopDBEntities db = new shopDBEntities();
        // GET: Market



        public ActionResult Index(string sellerID = null)
        {

            if (sellerID == null && Session["memberID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (sellerID == null && Session["memberID"] != null) //是非賣家會員
            {
                var a = Session["memberID"].ToString();
                if (db.Seller.Where(m => m.mbrID == a).Count() == 0)
                {
                    return RedirectToAction("SellerCreate", "SellerCertification");
                }
            }

            //if (Session["memberID"].ToString() != null && db.Seller.Where(m => m.mbrID == Session["memberID"].ToString()).Count() == 0) 
            //{

            //        return RedirectToAction("SellerCreate", "SellerCertification");

            //}


            string selID;
            selID = sellerID;
            if (selID == null)
            {

                var id = Session["memberID"].ToString();
                selID = (from m in db.Seller
                         where m.mbrID == id
                         select m).FirstOrDefault().selID;
            }

            ViewBag.Stars = db.Review.Where(m => m.Product.selID == selID).Count() == 0 ? 0 : db.Review.Where(m => m.Product.selID == selID).Average(m => m.rvwStar);

            var pdtID = from m in db.Product
                        where m.selID == selID
                        select m.pdtID;


            VM_Market mk = new VM_Market()
            {
                sellers = db.Seller.Where(m => m.selID == selID).ToList(),
                products = db.Product.Where(m => m.selID == selID).ToList(),
                reviews = db.Review.Where(m => m.Order.selID == selID).ToList(),
            };

            ViewBag.fstLayer = db.Product.Where(m => m.selID == selID).Select(m => m.ProductCategory.FirstLayer).Distinct().ToList();


            return View(mk);
        }

        public PartialViewResult _rvwIndex(int star, string sellerID)
        {
            string getselID = sellerID;

            List<string> odrid = (from m in db.Order
                                  where m.selID == getselID
                                  select m.odrID).ToList();
            if (star == 6)
            {
                var rvwodrid2 = db.Review.Where(o => o.Order.selID == getselID).ToList();


                return PartialView(rvwodrid2);

            }


            var rvwodrid = db.Review.Where(o => o.Order.selID == getselID).Where(o => o.rvwStar == star).ToList();


            return PartialView(rvwodrid);




        }




    }
}