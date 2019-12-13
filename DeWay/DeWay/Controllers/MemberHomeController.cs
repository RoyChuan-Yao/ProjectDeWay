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

namespace DeWay.Controllers
{
    public class MemberHomeController : Controller
    {
        shopDBEntities db = new shopDBEntities();
        // GET: MemberHome 

        public ActionResult mbrIndex()
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");

            string id = Session["memberID"].ToString();

            var member = db.Member.Where(m => m.mbrID == id).ToList();

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
        public ActionResult pwdEdit(string id)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");

            var account = db.MemberAccount.Find(id);
            return View(account);


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


            return View(account);




        }
        public ActionResult odrIndex(string odrStatus )
        {

            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");

            string id = Session["memberID"].ToString();

            ViewBag.odrStatus = "目前尚未有"+odrStatus+"的訂單!";

            List<string> orderID = (from o in db.Cart_OrderDetail
                                    where o.mbrID == id
                                    select o.odrID
                                 ).ToList();
            string state = db.OrderStatus.Where(m => m.odrStatus == odrStatus).FirstOrDefault().odrStatusID.ToString();
            List<string> orderStateGroup = new List<string>();

           
            switch (state)
            {
                case "ods0000001":
                    orderStateGroup.Add( "ods0000001");
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
                    orderStateGroup.Add("ods0000006");
                    break;
                case "ods0000007":
                    orderStateGroup.Add("ods0000007");
                    break;
                case "ods0000008":
                    orderStateGroup.Add("ods0000008");
                    break;
                case "ods0000009":
                    orderStateGroup.Add("ods0000009");
                    break;

            }


            var result = db.Order
                .Where(m => orderID.Contains(m.odrID))
                .Where(m => orderStateGroup.Contains(m.OrderStatus.odrStatusID)).ToList();
  
            return View(result);



            //IEnumerable<object> result ;
            //if (code == 0)
            //{
            //     result = db.Order.Where(o => orderID.Contains(o.odrID) && o.odrStatusID == "ods0000001").ToList();
            //}

            //else if (code == 1)
            //{
            //     result = db.Order.Where(o => orderID.Contains(o.odrID) && o.odrStatusID == "ods0000002").ToList();
            //}
            //else if (code == 2)
            //{
            //     result = db.Order.Where(o => orderID.Contains(o.odrID) && o.odrStatusID. ).ToList();
            //}
            //else
            //{

            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //return View(result);



        }
        
        public ActionResult QAIndex(int code = 0)

        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            string id = Session["memberID"].ToString();
            ViewBag.MemberName = db.Member.Where(m => m.mbrID == id).FirstOrDefault().nickName;

            if (code == 0)
            {
                var qaall = db.QA.Where(m => m.mbrID == id).ToList().OrderByDescending(m => m.qaTime);
                return View(qaall);
            }
            else if (code == 1)
            {
                var qayet = db.QA.Where(m => m.mbrID == id && (m.Answer == null || m.Answer == "")).ToList().OrderByDescending(m => m.qaTime);
                return View(qayet);
            }
            else if (code == 2)
            {
                var qaed = db.QA.Where(m => m.mbrID == id && m.Answer != null && m.Answer != "").ToList().OrderByDescending(m => m.qaTime);
                return View(qaed);
            }
            return HttpNotFound();



        }
        public ActionResult rvwIndex()
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }


        public PartialViewResult _rvwIndex(int code = 0, string odr = "")
        {


            string id = Session["memberID"].ToString();
            List<string> odrid = (from m in db.Cart_OrderDetail
                                  where m.mbrID == id && m.odrID != null
                                  select m.odrID).ToList();
            var rvwodrid = (from o in db.Review
                            where o.mbrID == id
                            select o.odrID).ToList();
            if (code == 0)
            {


                var nonreview = db.Order.Where(m => !rvwodrid.Contains(m.odrID) && odrid.Contains(m.odrID)).ToList();

                return PartialView(nonreview);



            }
            else if (code == 1)
            {

                var reviewed = db.Order.Where(m => rvwodrid.Contains(m.odrID)).ToList();
                return PartialView(reviewed);
                
            }

            else if (code == 2)
            {
                ViewBag.odr = odr;
                var odrreview = db.Order.Where(m => m.odrID == odr).ToList();
                return PartialView(odrreview);
                
            }
            return ViewBag.message;



        }

        public string getImage(string id)
        {
            var getSpcID = db.Cart_OrderDetail.Where(m => m.odrID == id).FirstOrDefault().spcID;
            var getPdtID = db.Specification.Where(m => m.spcID == getSpcID).FirstOrDefault().pdtID;
            var getPdtImg = db.ProductImage.Where(m => m.pdtID == getPdtID).FirstOrDefault().pdtImage;



            return getPdtImg;

        }


        public ActionResult rvwCreate(string odrID)
        {



            ViewBag.odrID = odrID;
            return View("rvwCreate");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult rvwCreate(string odrID, string rvwContent,short rvwStar, int code = 0)
        {
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


            return RedirectToAction("rvwIndex");
        }
        public ActionResult odrDetail(string odrID)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            var odrdetail = db.Cart_OrderDetail.Where(o => o.odrID == odrID).ToList();
            
            return View(odrdetail);

        }

    }

  
}