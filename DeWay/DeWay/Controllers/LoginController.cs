using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace DeWay.Controllers
{
    public class LoginController : Controller
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["shopDBConnectionString"].ConnectionString);
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string id, string pwd)
        {
            string sql = "select * from MemberAccount where mbrAccount = @mbrAccount and mbrPwd=@mbrPwd";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            cmd.Parameters.AddWithValue("@mbrAccount", id);
            cmd.Parameters.AddWithValue("@mbrPwd", pwd);
            SqlDataReader rd;

            Conn.Open();
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Session["id"] = rd["mbrID"].ToString();


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
    }
}