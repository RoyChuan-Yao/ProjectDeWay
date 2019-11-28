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
            Product product = db.Product.Where(m => m.pdtID == pdtID).FirstOrDefault();
            //TODO : 記得刪除viewmodel
            //取出相關商品單筆資料

            /*TODO : 被查詢商品必須是上架的
             假如沒上架：引導至查無商品頁面*/
            /*商品加入購物車：
             1. 點選規格
             2. 點選購買數
             3. 點選加入購物車(按鈕之後變成前往購物車)
             */
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
