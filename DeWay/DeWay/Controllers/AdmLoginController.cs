using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using DeWay.Models;

namespace DeWay.Controllers
{
    public class AdmLoginController : Controller
    {
       
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["shopDBConnectionString"].ConnectionString);
        // GET: AdmLogin
        public ActionResult Index()
        {
            return View();
        }
         
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(string id, string pwd)
        {
            string sql = "select * from AdmAccount where admAct = @admAct and admPwd=@admPwd";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            cmd.Parameters.AddWithValue("@admAct", id);
            cmd.Parameters.AddWithValue("@admPwd", pwd);
            SqlDataReader rd;

            Conn.Open();
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Session["AdmID"] = rd["admID"].ToString();


                Conn.Close();
                return RedirectToAction("Index", "AdmCenter");
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
            return RedirectToAction("Index", "AdmLogin");
        }
    }
}