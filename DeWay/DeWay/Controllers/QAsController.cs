using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;

namespace DeWay.Controllers
{
    //TODO
    //編輯QA
    //刪除QA
    public class QAsController : Controller
    {
        private shopDBEntities db = new shopDBEntities();

        //QA 部分檢視(用於:商品專頁、會員中心、賣家中心)
        public PartialViewResult _GetQASection(string productID)
        {
            List<QA> qa = db.QA.Where(m => m.pdtID == productID).OrderByDescending(m=>m.qaTime).ToList(); // 取出商品的QA內容
            List<string> memberID = qa.Select(m => m.mbrID).ToList();//取出發問者ID
            Dictionary<string, string> memberNames = new Dictionary<string, string>();
            foreach (string m in memberID)
            {
                string memberName = db.Member.Where(n => n.mbrID == m).FirstOrDefault().mbrName;
                memberNames[m] = memberName;
            }
            string mbrID = (string)Session["memberID"];

            var checksel = db.Seller.Where(m => m.mbrID == mbrID).Count();
            //應急處理 不然登入者不是賣家會報錯

            if (mbrID != null && checksel!=0)
            {
                ViewBag.sellID = db.Seller.Where(s => s.mbrID == mbrID).FirstOrDefault().selID;
            }
            ViewBag.memberNames = memberNames;//建成字典檔用於VIEW中調用
            return PartialView(qa);
        }
        [ChildActionOnly]
        public PartialViewResult _CreateQA(string productID)
        {
           
           
            ViewBag.productID = productID;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQA(string question, string pdtID)
        {
            string GetQAID = db.Database.SqlQuery<string>("Select dbo.GetQAID()").FirstOrDefault();
            var qa = new QA()
            {
                qaID = GetQAID,
                mbrID = (string)Session["memberID"],
                Question = question,//TODO:限制長度
                pdtID = pdtID,
                displayStatus = true,
                qaTime = DateTime.Now,
            };
            //QA 預設值
            //qaID  : 自動產生 TODO : QAID不可為NULL
            //qa未讀 : 預設未讀
            //qa公開 : 預設不公開

            db.QA.Add(qa);
            db.SaveChanges();
            return RedirectToAction("_GetQASection", new { productID = pdtID });
        }

        [HttpPost]
        public ActionResult replyMessage(string qaID,string Answer)
        {
            var getpdtID = db.QA.Where(m => m.qaID == qaID).FirstOrDefault().pdtID;
            var qa = db.QA.Where(m => m.qaID == qaID).FirstOrDefault();
            qa.Answer = Answer;
            db.SaveChanges();

            return RedirectToAction("ProductPage", "ProductPage", new { pdtID = getpdtID });


        }
    }

}
