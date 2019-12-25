using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;

namespace DeWay.Controllers
{
    public class SellerCertificationController : Controller
    {
        shopDBEntities db = new shopDBEntities();
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["shopDBConnectionString"].ConnectionString);
        SqlCommand Cmd = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();

        private void executeSql(string sql)
        {
            Cmd.CommandText = sql;
            Cmd.Connection = Conn;

            Conn.Open();
            Cmd.ExecuteNonQuery();
            Conn.Close();
        }

        private DataTable querySql(string sql)
        {

            Cmd.CommandText = sql;
            Cmd.Connection = Conn;
            adp.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            adp.Fill(ds);

            return ds.Tables[0];
        }


        // GET: SellerCertification
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SellerCreate()
        {
            


            if (Session["memberID"] == null)
            {
                return RedirectToAction("Login", "Login"); }


            string a = Session["memberID"].ToString();

            if (db.Seller.Where(m =>m.mbrID==a).Count()>0)
            {
                return RedirectToAction("OrderIndex", "SellerHome");
            }


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SellerCreate(Seller seller, HttpPostedFileBase photo) //創造賣家
        {
            if (ModelState.IsValid !=true)
            {
                return View();
            }

                string fileName = "";
            string GetSellerID = db.Database.SqlQuery<string>("Select dbo.GetSellerID()").FirstOrDefault();
            string mbrID = Session["memberID"].ToString();
            seller.selID = GetSellerID;
            seller.mbrID = mbrID;
            seller.selImage = GetSellerID + ".jpg";
            seller.selAut = "2";
           
         
                HttpPostedFileBase f = (HttpPostedFileBase)photo;
                if(f!=null)
                {
                    if(f.ContentLength > 0)
                    {
                        fileName = GetSellerID + ".jpg";
                        f.SaveAs(Server.MapPath("~/sellerImage/" + fileName));
                    }
                }

            if (f == null)
            {
                seller.selImage = "sel0000000.jpg";
            }
            

                if (ModelState.IsValid)
            {
                db.Entry(seller).State = EntityState.Modified;
                if(lastcheck(seller.IDNumber) == true)
                { 
                db.Seller.Add(seller);
                db.SaveChanges();
                return RedirectToAction("Index", "Market");
                }
                else
                {
                    ViewBag.Message = "身份證字號不合法";                   
                }
            }
            return View(seller);



        }
        public ActionResult IDNumber(string mbrID)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            

            mbrID = Session["memberID"].ToString();

            var getselID = db.Seller.Where(m => m.mbrID == mbrID).FirstOrDefault().selID;

            var getSeller = db.Seller.Where(m => m.selID == getselID).FirstOrDefault();





            return View(getSeller);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IDNumber(Seller seller) //身分證字號
        {
            string mbrID = Session["memberID"].ToString();

            var getselID = db.Seller.Where(m => m.mbrID == mbrID).FirstOrDefault().selID;

            var getSeller = db.Seller.Where(m => m.selID == getselID).FirstOrDefault();

            getSeller.IDNumber = seller.IDNumber;
            if (lastcheck(getSeller.IDNumber) == true)
            {
                db.SaveChanges();
                return RedirectToAction("mbrIndex", "MemberHome");
            }
            else
            {
                ViewBag.Message = "身份證字號不合法";
            }
            return RedirectToAction("mbrIndex", "MemberHome");
        }

        public ActionResult GUINumber(string mbrID)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");

            mbrID = Session["memberID"].ToString();

            var getselID = db.Seller.Where(m => m.mbrID == mbrID).FirstOrDefault().selID;

            var getSeller = db.Seller.Where(m => m.selID == getselID).FirstOrDefault();





            return View(getSeller);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GUINumber(Seller seller) //公司名與公司號
        {


            string mbrID = Session["memberID"].ToString();

            var getselID = db.Seller.Where(m => m.mbrID == mbrID).FirstOrDefault().selID;

            var getSeller = db.Seller.Where(m => m.selID == getselID).FirstOrDefault();

            getSeller.GUINumber = seller.GUINumber;
            getSeller.selCompany = seller.selCompany;

            db.SaveChanges();

            return RedirectToAction("mbrIndex", "MemberHome");
        }

        public bool lastcheck(string ID) //身分證合法性驗證
        {
            int num = 0;
            string eng = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
            int[] a = new int[11];
            a[2] = Int32.Parse(ID[1].ToString()); a[3] = Int32.Parse(ID[2].ToString()); a[4] = Int32.Parse(ID[3].ToString());
            a[5] = Int32.Parse(ID[4].ToString()); a[6] = Int32.Parse(ID[5].ToString()); a[7] = Int32.Parse(ID[6].ToString());
            a[8] = Int32.Parse(ID[7].ToString()); a[9] = Int32.Parse(ID[8].ToString()); a[10] = Int32.Parse(ID[9].ToString());
            for (int i = 0; i <= 25; i++)
            {
                if (ID[0] == eng[i])
                {
                    num = i + 10;
                    a[0] = num / 10;
                    a[1] = num % 10;
                }
            }
            int total = a[0] * 1 + a[1] * 9 + a[2] * 8 + a[3] * 7 + a[4] * 6 + a[5] * 5 + a[6] * 4 + a[7] * 3 + a[8] * 2 + a[9] * 1 + a[10];

            if (total % 10 == 0)
                return true;
            else
                return false;
        }


    }

}