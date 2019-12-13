using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

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

        public ActionResult IDNumber(string selID)
        {
            //var sel = db.Seller.Where(m => m.selID == selID).FirstOrDefault();
            //     var aut = db.Seller.Where(m => m.selID == selID).FirstOrDefault().SellerAut.ToString();
          
                if (db.Seller.Where(m => m.selID == selID).FirstOrDefault() != null && Session["memberID"] != null) //暫定Session與selID
            {
                Cmd.Parameters.AddWithValue("@selID", selID);

               
                Seller seller = new Seller();

                
                return View(seller);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult IDNumber(Seller seller)
        {

            string sql = "Update Seller set IDNumber=@IDNumber" +
                " where selID=@selID";
            //Session["memberID"] = seller.mbrID;
            Cmd.Parameters.AddWithValue("@selID", seller.selID);

            
           


            Cmd.Parameters.AddWithValue("@IDNumber", seller.IDNumber);
            

            executeSql(sql);

            return RedirectToAction("Index");
        }

        public ActionResult GUINumber(string selID)
        {
            Cmd.Parameters.AddWithValue("@selID", selID);
            
            
            Seller seller = new Seller();



            return View(seller);
        }
        [HttpPost]
        public ActionResult GUINumber(Seller seller)
        {

            
            string sql = "Update Seller set selCompany=@selCompany," +
                "  GUINumber=@GUINumber" +
                " where selID=@selID";
            //Session["memberID"] = seller.mbrID;
            Cmd.Parameters.AddWithValue("@selID", seller.selID);
            
            Cmd.Parameters.AddWithValue("@selCompany", seller.selCompany);

            Cmd.Parameters.AddWithValue("@GUINumber", seller.GUINumber);
            

            executeSql(sql);

            return RedirectToAction("Index");
        }
       

    }

}