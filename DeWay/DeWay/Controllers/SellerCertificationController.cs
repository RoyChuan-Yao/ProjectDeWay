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

        shopDBEntities db = new shopDBEntities();
        // GET: SellerCertification
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IDNumber(string selID)
        {
            Cmd.Parameters.AddWithValue("@selID", selID);
            DataRow dt = querySql("select * from Seller where selID=@selID").Rows[0];
            //string str = "";
            Seller seller = new Seller();
            
            seller.selID = (String)dt["selID"];
            //seller.selCompany = (String)dt["selCompany"];
            seller.selCity = (String)dt["selCity"];
            seller.IDNumber = dt["IDNumber"].ToString();
            seller.selInfo = (String)dt["selInfo"];
            seller.selAddress = (String)dt["selAdress"];
            seller.selDist = (String)dt["selDist"];

            return View(seller);
        }

        [HttpPost]
        public ActionResult IDNumber(Seller seller)
        {
            string sql = "Update Seller set selCity=@selCity, selDist=@selDist, selAdress=@selAdress, IDNumber=@IDNumber, selInfo=@selInfo" +
                " where selID=@selID";
            //Session["memberID"] = seller.mbrID;
            Cmd.Parameters.AddWithValue("@selID", seller.selID);
            Cmd.Parameters.AddWithValue("@selCity", seller.selCity);
            Cmd.Parameters.AddWithValue("@selDist", seller.selDist);
            Cmd.Parameters.AddWithValue("@selAdress", seller.selAddress);
            Cmd.Parameters.AddWithValue("@IDNumber", seller.IDNumber);
            Cmd.Parameters.AddWithValue("@selInfo", seller.selInfo);

            executeSql(sql);

            return RedirectToAction("Index");
        }

        public ActionResult GUINumber(string selID)
        {
            Cmd.Parameters.AddWithValue("@selID", selID);
            DataRow dt = querySql("select * from Seller where selID=@selID").Rows[0];
            
            Seller seller = new Seller();

            seller.selID = (String)dt["selID"];
            seller.selCompany = dt["selCompany"].ToString();
            seller.selCity = (String)dt["selCity"];
            seller.IDNumber = dt["GUINumber"].ToString();
            seller.selInfo = (String)dt["selInfo"];
            seller.selAddress = (String)dt["selAddress"];
            seller.selDist = (String)dt["selDist"];

            return View(seller);
        }
        [HttpPost]
        public ActionResult GUINumber(Seller seller)
        {
            string sql = "Update Seller set selCity=@selCity, selDist=@selDist, selCompany=@selCompany," +
                " selAdress=@selAdress, GUINumber=@GUINumber, selInfo=@selInfo" +
                " where selID=@selID";
            //Session["memberID"] = seller.mbrID;
            Cmd.Parameters.AddWithValue("@selID", seller.selID);
            Cmd.Parameters.AddWithValue("@selCity", seller.selCity);
            Cmd.Parameters.AddWithValue("@selDist", seller.selDist);
            Cmd.Parameters.AddWithValue("@selCompany", seller.selCompany);
            Cmd.Parameters.AddWithValue("@selAdress", seller.selAddress);
            Cmd.Parameters.AddWithValue("@GUINumber", seller.GUINumber);
            Cmd.Parameters.AddWithValue("@selInfo", seller.selInfo);

            executeSql(sql);

            return RedirectToAction("Index");
        }

        
    }

}