using DeWay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                tempPwd = account.mbrPwd;

                if (tempPwd == oldPwd)
                {

                    account.mbrPwd = mbrPwd;


                    if (ModelState.IsValid)
                    {
                        db.SaveChanges();
                        return RedirectToAction("mbrIndex");
                    }
                  

                }
               

                    return View(account);
                
            }
            return RedirectToAction("Login", "Login");

        }
    }
}