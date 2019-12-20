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
        public ActionResult Index()
        {
            var id = Session["memberID"].ToString();
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
                reviews = db.Review.Where(m => m.Order.selID == selID).ToList(),
            };

            return View(mk);
        }

        public PartialViewResult _rvwIndex(int star)
        {
            string id = Session["memberID"].ToString();
            string getselID = db.Seller.Where(m => m.mbrID == id).FirstOrDefault().selID;

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