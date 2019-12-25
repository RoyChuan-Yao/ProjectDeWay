using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using DeWay.Models;
using System.Data.Entity;

namespace DeWay.Controllers
{
    public class LoginController : Controller
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["shopDBConnectionString"].ConnectionString);
        shopDBEntities db = new shopDBEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string id, string pwd)
        {
            var checkmbr = db.Member.Where(m => m.MemberAccount.mbrAccount == id).Count();

            if (checkmbr != 0)
            {
                var getmbrBlock = db.Member.Where(m => m.MemberAccount.mbrAccount == id).FirstOrDefault().mbrBlock;

                if (getmbrBlock == true)
                {
                    ViewBag.LoginErr = "您已被停權!!!";

                    return View();

                }
            }
            string sql = "select * from MemberAccount where mbrAccount = @mbrAccount and mbrPwd=@mbrPwd";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            cmd.Parameters.AddWithValue("@mbrAccount", id);
            cmd.Parameters.AddWithValue("@mbrPwd", pwd);
            SqlDataReader rd;
            Conn.Open();
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Session["memberID"] = rd["mbrID"].ToString();
                

                Conn.Close();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Conn.Close();
                ViewBag.LoginErr = "帳號或密碼有誤";

                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Member mbr, MemberAccount memberAccount)
        {
            var a = ModelState.IsValid;

            string GetMemberID = db.Database.SqlQuery<string>("Select dbo.GetMemberID()").FirstOrDefault();
            mbr.mbrID = GetMemberID;
            memberAccount.mbrID = GetMemberID;

            mbr.nickName = mbr.mbrName;
            mbr.Points = 0;
            mbr.mbrAut = false;
            mbr.signupDate = DateTime.Now;
            mbr.mbrImage = "0.jpg";
            mbr.mbrBlock = false;
            mbr.MemberAccount = memberAccount;


            if (ModelState.IsValid)
            {
                //db.Entry(mbr).State = EntityState.Modified;
                //db.Entry(mbr).State = EntityState.Detached;

                db.Member.Add(mbr);
                db.SaveChanges();
                //db.MemberAccount.Add(memberAccount);
                //db.SaveChanges();



                return RedirectToAction("Registercheck", "login");
            }
            ViewBag.RegisterErr = true;
            return View("Login");
        }

        public ActionResult Registercheck()
        {
            return View();
        }
    }
}