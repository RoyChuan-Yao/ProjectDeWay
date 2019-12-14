using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;

namespace DeWay.Controllers
{

    public class SellerHomeController : Controller
    {
        shopDBEntities db = new shopDBEntities();
        // GET: SellerHome
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult OrderIndex(string odrStatus = null)
        {

            string id = Session["memberID"].ToString();
            string getselID = db.Seller.Where(m => m.mbrID == id).FirstOrDefault().selID;

            if (odrStatus == null)
            {


                var AllOrder = db.Order.Where(m => m.selID == getselID).ToList();

                return View(AllOrder);

            }


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
                .Where(m => m.selID == getselID)
                .Where(m => orderStateGroup.Contains(m.OrderStatus.odrStatusID)).ToList();




            return View(result);
        }
    }

}