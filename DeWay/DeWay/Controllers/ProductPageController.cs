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
            string memberSellID = db.Seller.Where(m => m.mbrID == memberID).Select(m => m.mbrID).ToString();

            var checksel = db.Seller.Where(m => m.mbrID == memberID).Count();
            //應急處理 不然登入者不是賣家會報錯
            ViewBag.Stars = product.Review.Count() == 0 ? 0 : product.Review.Average(m => m.rvwStar);

            string selID = "";
            if (memberID != null && checksel != 0)
            {
                selID = db.Seller.Where(m => m.mbrID == memberID).FirstOrDefault().selID;
            }
            //商品尚未上架 且 不是該商品賣家 則回傳失敗
            if (product.Discontinued && product.selID != selID)
            {
                return HttpNotFound();
                //TODO： 製作404頁面
            }
            return View(product);
        }
        public int GetProductStock(string specID)
        {
            int stockResult = db.Specification
                .Where(m => m.spcID == specID)
                .FirstOrDefault().Stock;
            stockResult = stockResult >= 20 ? 20 : stockResult; //庫存超過20 則返回20
            return stockResult;
        }
        public PartialViewResult _GetProductCard(string productID)
        {
            var product = db.Product.Find(productID);
            return PartialView(product);
        }


        public PartialViewResult _GetProductCardFst(string productID, string fstID)
        {
            var product = db.Product.Where(m => m.pdtID == productID && m.ProductCategory.fstLayerID == fstID).FirstOrDefault();
            return PartialView("_GetProductCard", product);
        }

        public PartialViewResult _GetSetOfProductCard(int takeCount, int skipCount = 0)
        {
            var pdts = db.Product.Where(m => m.Discontinued != true).OrderBy(m => m.pdtID).Skip(skipCount).Take(takeCount);
            return PartialView(pdts);
        }
    }
}
