using DeWay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.ViewModels;
using DeWay.Email;

namespace DeWay.Controllers
{
    public class MemberHomeController : Controller
    {
        shopDBEntities db = new shopDBEntities();
        // GET: MemberHome 
        public ActionResult mbrIndex()
        {
            if (Session["memberID"] != null)
            {
                string id = Session["memberID"].ToString();

                var member = db.Member.Where(m => m.mbrID == id).ToList();

                return View(member);
            }
            return RedirectToAction("Login", "Login");
        }
        public ActionResult mbrEdit(string id)
        {
            if (Session["memberID"] != null)
            {
                
                var member = db.Member.Where(m => m.mbrID == id).FirstOrDefault();
                return View(member);
        }
            return RedirectToAction("Login", "Login");
    }
        [HttpPost]
        public ActionResult mbrEdit(Member m,HttpPostedFileBase Image)
        {
            if (Session["memberID"] != null)
            {
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
                member.birthDate =m.birthDate;
                member.mbrImage = fileName;





            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("mbrIndex");
            }

            else
                return View(m);


        }
            return RedirectToAction("Login", "Login");

        }
        public ActionResult pwdEdit(string id)
        {
           if (Session["memberID"] != null) { 
            var account = db.MemberAccount.Find(id);
            return View(account);
            }
            return RedirectToAction("Login", "Login");
        }
        [HttpPost]
        public ActionResult pwdEdit(string id, string oldPwd, string mbrPwd)
        {
            if (Session["memberID"] != null)
            {
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
                        gs.receiver =$"{member.mbrMail}";  //收件人mail  用到變數時前面加$
                        gs.subject = "密碼更改通知";  //標題
                        gs.messageBody = $"<html><body>{member.mbrName}您好:<br />您的密碼已做更改，若您沒有近期沒有更改密碼，請盡速與我們聯繫。</body></html>";  //內容
                        gs.IsHtml = true;  //內容是否使用html
                        gs.Send();
                        return RedirectToAction("mbrIndex");
                    }


                }
               

                    return View(account);
                
            }
            return RedirectToAction("Login", "Login");

        }
        public ActionResult odrIndex()
        {


            var cartOrder = db.Cart_OrderDetail.Where(m => m.mbrID == "mbr0000001" && m.odrID != null).ToList();
            List<string> odrList = (from m in db.Cart_OrderDetail
                                    where m.mbrID == "mbr0000001"
                                    select m.odrID).ToList();

            List<string> odrSpcList = (from m in db.Cart_OrderDetail
                                    where m.mbrID == "mbr0000001"
                                    select m.spcID).ToList();

            List<string> specList = (from s in db.Specification
                                     where odrSpcList.Any(a=>a==s.spcID)
                                     select s.pdtID).ToList();

            VM_MH_AllOrders AllOrders = new VM_MH_AllOrders()
            {

                Order = db.Order.Where(m => odrList.Contains(m.odrID)).ToList(),
                OrderDetail = db.Cart_OrderDetail.Where(m => odrList.Contains(m.odrID)).ToList(),
                Specification = db.Specification.Where(m => odrList.Contains(m.spcID)).ToList(),
                Product = db.Product.Where(m => specList.Contains(m.pdtID)).ToList()


            };

            return View(AllOrders);




        }
    }
}