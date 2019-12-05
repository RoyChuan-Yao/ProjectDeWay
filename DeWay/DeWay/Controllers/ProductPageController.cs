using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;

namespace DeWay.Controllers
{
    public class ProductPageController : Controller
    {
        // GET: ProductPage
        //商品專頁
        shopDBEntities db = new shopDBEntities();
        public ActionResult ProductPage(string pdtID = "pdt0000001")
        {
            var product = db.Product.Where(m => m.pdtID == pdtID).FirstOrDefault();
            string memberID = (string)Session["memberID"];
            string selID="";
            if (memberID != null) 
            {
                selID = db.Seller.Where(m => m.mbrID == memberID).FirstOrDefault().selID;
            }
            //商品尚未上架 且 不是該商品賣家 則回傳失敗
            if (product.Discontinued && product.selID != selID)
            {
                return HttpNotFound();
                //TODO ：製作404頁面
            }
            return View(product);
        }
        public string GetProductStock(string specID)
        {
            string stockResult = db.Specification
                .Where(m => m.spcID == specID)
                .FirstOrDefault().Stock;
            return stockResult;
        }

    }
}
