using DeWay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeWay.Controllers
{
    public class ReviewController : Controller
    {
        private shopDBEntities db = new shopDBEntities();

        //QA 部分檢視(用於:商品專頁、會員中心、賣家中心)
        public PartialViewResult _GetReviewSection(string productID)
        {
            var review = db.Review.Where(m => m.pdtID == productID).OrderByDescending(m => m.rvwTime).ToList(); 
            return PartialView(review);
        }
    }
}
