using DeWay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.ViewModels;
using DeWay.Email;
using System.Net;
using System.Data.Entity.Infrastructure;

namespace DeWay.Controllers
{
    public class MemberHomeController : Controller
    {
        shopDBEntities db = new shopDBEntities();
        // GET: MemberHome 

        public ActionResult mbrIndex(string mbrID = null)
        {
            


            if (mbrID == null && Session["memberID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (mbrID == null && Session["memberID"] != null) 
            {
                var a = Session["memberID"].ToString();
                var member1 = db.Member.Where(m => m.mbrID == a).ToList();
                ViewBag.id = Session["memberID"].ToString();

                return View(member1);

            }
            string memberID;
            memberID = mbrID;
            if (memberID == null)
            {

                memberID = Session["memberID"].ToString();


            }
            if (Session["memberID"] != null && Session["memberID"].ToString() == memberID)
            {
                ViewBag.id = Session["memberID"].ToString();
            }
            else
            {
                ViewBag.id = null;
            }

            var member = db.Member.Where(m => m.mbrID == memberID).ToList();




            return View(member);

        }
        public ActionResult mbrEdit(string id)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");


            var member = db.Member.Where(m => m.mbrID == id).FirstOrDefault();
            return View(member);


        }
        [HttpPost]
        public ActionResult mbrEdit(Member m, HttpPostedFileBase Image)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");


            string fileName = "";

            if (Image != null)
            {
                if (Image.ContentLength > 0)
                {

                    fileName = m.mbrID + ".jpg";
                    Image.SaveAs(Server.MapPath("~/mbrPhoto/" + fileName));
                }
            }
            else
            {
                fileName = m.mbrImage;
            }


            var member = db.Member.Where(a => a.mbrID == m.mbrID).FirstOrDefault();
            member.mbrName = m.mbrName;
            member.nickName = m.nickName;
            member.mbrPhone = m.mbrPhone;
            member.mbrMail = m.mbrMail;
            member.birthDate = m.birthDate;
            member.mbrImage = fileName;





            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("mbrIndex");
            }

            else
                return View(m);





        }
        public ActionResult pwdEdit()
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");


            ViewBag.id = Session["memberID"].ToString();
            return View();



        }
        [HttpPost]
        public ActionResult pwdEdit(string id, string oldPwd, string mbrPwd)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            string tempPwd = "";
            var account = db.MemberAccount.Find(id);
            var member = db.Member.Find(id);
            tempPwd = account.mbrPwd;

            if (tempPwd == oldPwd)
            {

                account.mbrPwd = mbrPwd;


                if (ModelState.IsValid)
                {
                    db.SaveChanges();

                    GmailSender gs = new GmailSender();
                    gs.account = "qoo61191910@gmail.com";  //帳號
                    gs.password = "jdfxkfgbjibhqsvz";  //應用程式密碼
                    gs.sender = "qoo61191910@gmail.com";  //寄件人mail
                    gs.receiver = $"{member.mbrMail}";  //收件人mail  用到變數時前面加$
                    gs.subject = "密碼更改通知";  //標題
                    gs.messageBody = $"<html><body>{member.mbrName}您好:<br />您的密碼已做更改，若您沒有近期沒有更改密碼，請盡速與我們聯繫。</body></html>";  //內容
                    gs.IsHtml = true;  //內容是否使用html
                    gs.Send();
                    return RedirectToAction("mbrIndex");
                }


            }
            ViewBag.error = "舊密碼不符";
            ViewBag.id = Session["memberID"].ToString();
            return View();




        }
        public ActionResult odrIndex(string odrStatus = "未付款")
        {

            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");

            string id = Session["memberID"].ToString();

            ViewBag.odrStatus = "目前尚未有" + odrStatus + "的訂單!";

            List<string> orderID = (from o in db.Cart_OrderDetail
                                    where o.mbrID == id
                                    select o.odrID
                                 ).ToList();
            string state = db.OrderStatus.Where(m => m.odrStatus == odrStatus).FirstOrDefault().odrStatusID.ToString();
            List<string> orderStateGroup = new List<string>();


            switch (state)
            {
                case "ods0000001":
                    orderStateGroup.Add("ods0000001");
                    break;
                case "ods0000002":
                    orderStateGroup.Add("ods0000002");
                    break;
                case "ods0000004":
                    orderStateGroup.Add("ods0000003");
                    orderStateGroup.Add("ods0000004");
                    break;
                case "ods0000005":
                    orderStateGroup.Add("ods0000005");
                    break;
                case "ods0000006":
                    orderStateGroup.Add("ods0000006");
                    break;
                case "ods0000007":
                    orderStateGroup.Add("ods0000007");
                    break;
                case "ods0000008":
                    orderStateGroup.Add("ods0000008");
                    break;

            }


            var result = db.Order
                .Where(m => orderID.Contains(m.odrID))
                .Where(m => orderStateGroup.Contains(m.OrderStatus.odrStatusID)).ToList().OrderByDescending(m => m.odrDate);

            return View(result);





        }

        public ActionResult QAIndex(int code = 0)

        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            string id = Session["memberID"].ToString();
            ViewBag.MemberName = db.Member.Where(m => m.mbrID == id).FirstOrDefault().nickName;
            IEnumerable<object> qa;

            // 全部的問題
            if (code == 0)
            {
                qa = db.QA.Where(m => m.mbrID == id).ToList().OrderByDescending(m => m.qaTime);

            }
            // 未回覆的問題
            else if (code == 1)
            {
                qa = db.QA.Where(m => m.mbrID == id).Where(m => m.Answer == null || m.Answer == "").ToList().OrderByDescending(m => m.qaTime);

            }
            //已回覆的問題
            else if (code == 2)
            {
                qa = db.QA.Where(m => m.mbrID == id).Where(m => m.Answer != null).Where(m => m.Answer != "").ToList().OrderByDescending(m => m.qaTime);

            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(qa);



        }
        public ActionResult rvwIndex()
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _rvwIndex(int code = 0, string odr = "")
        {


            string id = Session["memberID"].ToString();
            List<string> odrid = (from m in db.Cart_OrderDetail
                                  where m.mbrID == id && m.odrID != null
                                  select m.odrID).ToList();
            var rvwodrid = (from o in db.Review
                            where o.mbrID == id
                            select o.odrID).ToList();
            IEnumerable<object> review;

            // 沒給評價
            if (code == 0)
            {

                review = db.Order.Where(m => !rvwodrid.Contains(m.odrID)).Where(m => odrid.Contains(m.odrID)).ToList();


            }
            // 已給評價
            else if (code == 1)
            {

                review = db.Order.Where(m => rvwodrid.Contains(m.odrID)).ToList();


            }
            // For odrDetail
            else if (code == 2)
            {
                ViewBag.code = code;
                review = db.Order.Where(m => m.odrID == odr).ToList();

            }
            else
                return ViewBag.message;

            return PartialView(review);

        }

        public string getImage(string id)
        {
            var getSpcID = db.Cart_OrderDetail.Where(m => m.odrID == id).FirstOrDefault().spcID;
            var getPdtID = db.Specification.Where(m => m.spcID == getSpcID).FirstOrDefault().pdtID;
            var getPdtImg = db.ProductImage.Where(m => m.pdtID == getPdtID).FirstOrDefault().pdtImage;



            return getPdtImg;

        }



        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult rvwCreate(string odrID, string rvwContent, short rvwStar, int code = 0)
        {

            //if (ModelState.IsValid != true)
            //{
            //    return View("rfdCreate");
            //}
            var pdtID = (from m in db.Cart_OrderDetail
                         where m.odrID == odrID
                         select m.Specification.pdtID).Distinct().ToList();
            string id = Session["memberID"].ToString();

            for (int i = 0; i < pdtID.Count(); i++)
            {
                Review reviewed = new Review();
                string GetReviewID = db.Database.SqlQuery<string>("Select dbo.GetReviewID()").FirstOrDefault();
                reviewed.rvwID = GetReviewID;
                reviewed.pdtID = pdtID[i];
                reviewed.rvwTime = DateTime.Now;
                reviewed.mbrID = id;
                reviewed.odrID = odrID;
                reviewed.rvwStar = rvwStar;
                reviewed.rvwContent = rvwContent;
                db.Review.Add(reviewed);
                db.SaveChanges();
            }

            if (code == 2)
                return RedirectToAction("odrDetail", new { odrID = odrID });
            return RedirectToAction("rvwIndex");
        }
        public ActionResult odrDetail(string odrID)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            ViewBag.refund = db.Refund.Where(o => o.odrID == odrID);
            var odrdetail = db.Cart_OrderDetail.Where(o => o.odrID == odrID).ToList();

            return View(odrdetail);

        }

        public ActionResult rfdIndex()
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            string id = Session["memberID"].ToString();
            var odrID = (from o in db.Cart_OrderDetail
                         where o.mbrID == id
                         select o.odrID).ToList();

            var refund = db.Refund.Where(r => odrID.Contains(r.odrID)).ToList();
            return View(refund);
        }



        public ActionResult rfdCreate(string odrID)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            var rfdcreate = db.Cart_OrderDetail.Where(o => o.odrID == odrID).ToList();
            return View(rfdcreate);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult rfdCreate(Refund Refund, RefundAccount RefundAccount)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            if (ModelState.IsValid != true)
            {

                var rfdcreate = db.Cart_OrderDetail.Where(o => o.odrID == Refund.odrID).ToList();
                return View(rfdcreate);
            }
            using (var transaction = db.Database.BeginTransaction())
            {
                Refund refund = new Refund();
                string GetRefundID = db.Database.SqlQuery<string>("Select dbo.GetRefundID()").FirstOrDefault();
                refund.rfdID = GetRefundID;
                refund.odrID = Refund.odrID;
                refund.rfdReason = Refund.rfdReason;
                refund.rfdProduct = Refund.rfdProduct;
                refund.rfdShipping = Refund.rfdShipping;
                refund.rfdShip = Refund.rfdShip;
                refund.rfdDate = DateTime.Now;
                refund.rfdStatusID = "rds0000001";
                db.Refund.Add(refund);
                try
                {
                    db.SaveChanges();

                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    return JavaScript($"alert({e.Message})");
                }

                RefundAccount refundAccount = new RefundAccount();

                refundAccount.rfdID = GetRefundID;
                refundAccount.bankCode = RefundAccount.bankCode;
                refundAccount.bankName = RefundAccount.bankName;
                refundAccount.bankAccount = RefundAccount.bankAccount;
                db.RefundAccount.Add(refundAccount);
                try
                {
                    db.SaveChanges();
                    transaction.Commit();
                    return RedirectToAction("rfdIndex");
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    return JavaScript($"alert({e.Message})");
                }



            }
        }
        public ActionResult rfdDetail(string rfdID)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            var odrID = (from r in db.Refund
                         where r.rfdID == rfdID
                         select r.odrID).ToList();

            VM_rfdDetail refundDetail = new VM_rfdDetail()
            {
                refund = db.Refund.Where(r => r.rfdID == rfdID).ToList(),
                cart_orderDetail = db.Cart_OrderDetail.Where(c => odrID.Contains(c.odrID)).ToList()
            };

            return View(refundDetail);
        }


        [HttpPost]
        public ActionResult changeStatus(string odrID, string odrStatus)
        {
            var odr = db.Order.Find(odrID);
            var odrstatus = "";
            switch (odrStatus)
            {
                case "未付款":
                    odrstatus = "ods0000002";
                    odrStatus = "處理中";
                    odr.pmtDate = DateTime.Now;
                    break;
                case "待取貨":
                    odrstatus = "ods0000006";
                    odrStatus = "已完成";
                    break;

            }



            odr.odrStatusID = odrstatus;
            db.SaveChanges();


            return RedirectToAction("odrIndex", new { odrStatus = odrStatus });
        }
    }


}