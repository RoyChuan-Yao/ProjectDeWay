using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.ViewModels;
using DeWay.Models;

namespace DeWay.Controllers
{
    public class Seller_MarketController : Controller
    {
        // GET: Seller_Maker
        shopDBEntities db = new shopDBEntities();

        public ActionResult sellerMarket()
        {
            var id = "mbr0000003";
            string selID = (from m in db.Seller
                            where m.mbrID == id
                            select m).FirstOrDefault().selID;

            var pdtID = from m in db.Product
                        where m.selID == selID
                        select m.pdtID;


            VM_Market mk = new VM_Market()
            {
                sellers = db.Seller.Where(m => m.selID == selID).ToList(),
                products = db.Product.Where(m => m.selID == selID).ToList(),
            };


            return View(mk);
        }

        public string getPdtImg(string pdtID)
        {

            var getPdtImg = db.ProductImage.Where(m => m.pdtID == pdtID).FirstOrDefault().pdtImage;



            return getPdtImg;
        }

        public string getPrice(string pdtID)
        {
            shopDBEntities db = new shopDBEntities();

            var getPrice = db.Specification.Where(m => m.pdtID == pdtID).FirstOrDefault().Price.ToString();

            return getPrice;
        }

    }
}