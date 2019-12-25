using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;
using DeWay.ViewModels;

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
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");




            string id = Session["memberID"].ToString();


            if (db.Seller.Where(m => m.mbrID == id).Count() == 0)
            {
                return RedirectToAction("SellerCreate", "SellerCertification");
            }


            string getselID = db.Seller.Where(m => m.mbrID == id).FirstOrDefault().selID;


            if (odrStatus == null)
            {


                var AllOrder = db.Order.Where(m => m.selID == getselID && m.odrStatusID != "ods0000008").ToList();

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
                    break;
                case "ods0000006":
                    orderStateGroup.Add("ods0000006");
                    break;
                case "ods0000007":
                    orderStateGroup.Add("ods0000007");
                    break;
                case "ods0000008":
                    orderStateGroup.Add("ods0000008");
                    break;

            }


            var result = db.Order
                .Where(m => m.selID == getselID)
                .Where(m => orderStateGroup.Contains(m.OrderStatus.odrStatusID)).ToList();




            return View(result);
        }

        public ActionResult odrDetail(string odrID)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            var odrdetail = db.Cart_OrderDetail.Where(o => o.odrID == odrID).ToList();

            decimal? total = 0;
            foreach (var item in odrdetail)
            {
                var subtotal = Math.Round((decimal)((int)item.pdtPrice * item.Discount * item.Quantity));

                total = total + subtotal;
                string newtotal = total.ToString().TrimEnd('0');

                if (newtotal.EndsWith("."))
                {
                    newtotal = newtotal.Substring(0, newtotal.Length - 1);
                }
            }

            ViewBag.total = total;
            ViewBag.status = db.Cart_OrderDetail.Where(o => o.odrID == odrID).FirstOrDefault().Order.odrStatusID;

            return View(odrdetail);

        }

        public ActionResult deliveryIndex(string shpMethod = null)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");



            string id = Session["memberID"].ToString();
            string getselID = db.Seller.Where(m => m.mbrID == id).FirstOrDefault().selID;

            if (shpMethod == null)
            {


                var AllOrder = db.Order.Where(m => m.selID == getselID && m.odrStatusID == "ods0000002").ToList();

                return View(AllOrder);

            }


            string state = db.Shipper.Where(m => m.shpMethod == shpMethod).FirstOrDefault().shpID.ToString();
            List<string> orderStateGroup = new List<string>();


            switch (state)
            {
                case "shp0000001":
                    orderStateGroup.Add("shp0000001");
                    break;
                case "shp0000002":
                    orderStateGroup.Add("shp0000002");
                    break;
                case "shp0000003":
                    orderStateGroup.Add("shp0000003");
                    break;
                case "shp0000004":
                    orderStateGroup.Add("shp0000004");
                    break;
                case "shp0000005":
                    orderStateGroup.Add("shp0000005");
                    break;

            }
            //var getodrID = (from m in db.Cart_OrderDetail
            //             where m.Order.selID == getselID
            //             select m.odrID).Distinct().ToList();

            var result = db.Order
                .Where(m => m.selID == getselID)
                .Where(m => m.odrStatusID == "ods0000002")
                .Where(m => orderStateGroup.Contains(m.Cart_OrderDetail.FirstOrDefault().Shipper.shpID)).ToList();




            return View(result);
        }



        public PartialViewResult _rvwIndex(string odrID)
        {


            string id = Session["memberID"].ToString();
            List<string> odrid = (from m in db.Cart_OrderDetail
                                  where m.mbrID == id && m.odrID != null
                                  select m.odrID).ToList();
            var rvwodrid = (from o in db.Review
                            where o.mbrID == id
                            select o.odrID).ToList();


            var odrreview = db.Order.Where(m => m.odrID == odrID).ToList();


            return PartialView(odrreview);
        }


        [HttpPost]
        public ActionResult delivery(string odrID, string traceNumber)
        {
            var editods = db.Order.Where(m => m.odrID == odrID).FirstOrDefault();
            editods.odrStatusID = "ods0000003";
            editods.traceNumber = traceNumber;
            editods.shpDate = DateTime.Now;

            //if (ModelState.IsValid == true)
            //{
            //    db.SaveChanges();
            //    return RedirectToAction("deliveryIndex");  //導向Index的Action方法
            //}
            db.SaveChanges();
            return RedirectToAction("deliveryIndex");  //導向Index的Action方法
        }

        [HttpPost]
        public ActionResult arrival(string odrID)
        {


            var editods = db.Order.Where(m => m.odrID == odrID).FirstOrDefault();
            editods.odrStatusID = "ods0000005";
            editods.odrDate = DateTime.Now;
            db.SaveChanges();



            return RedirectToAction("OrderIndex");
        }




        public ActionResult rfdIndex(string rfdStatus = null)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");

            string id = Session["memberID"].ToString();
            string getselID = db.Seller.Where(m => m.mbrID == id).FirstOrDefault().selID;


            var odrID = (from o in db.Order
                         where o.selID == getselID
                         select o.odrID).ToList();



            if (rfdStatus == null)
            {


                var refund = db.Refund.Where(r => odrID.Contains(r.odrID)).ToList();

                return View(refund);

            }

            string state = db.RefundStatus.Where(m => m.rfdStatus == rfdStatus).FirstOrDefault().rfdStatusID.ToString();
            List<string> orderStateGroup = new List<string>();


            switch (state)
            {
                case "rds0000001":
                    orderStateGroup.Add("rds0000001");
                    break;
                case "rds0000002":
                    orderStateGroup.Add("rds0000002");
                    break;
                case "rds0000003":
                    orderStateGroup.Add("rds0000003");
                    break;
                case "rds0000004":
                    orderStateGroup.Add("rds0000004");
                    break;


            }
            //var getodrID = (from m in db.Cart_OrderDetail
            //             where m.Order.selID == getselID
            //             select m.odrID).Distinct().ToList();

            var result = db.Refund
                .Where(m => odrID.Contains(m.odrID))
                .Where(m => orderStateGroup.Contains(m.rfdStatusID)).ToList();

            ViewBag.status = rfdStatus;


            return View(result);

        }
        public ActionResult rfdDetail(string rfdID)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            string id = Session["memberID"].ToString();
            string getselID = db.Seller.Where(m => m.mbrID == id).FirstOrDefault().selID;

            string RfdodrID = db.Refund.Where(r => r.rfdID == rfdID).FirstOrDefault().odrID;

            string odrID = db.Order.Where(o => o.selID == getselID).Where(o => RfdodrID.Contains(o.odrID)).FirstOrDefault().odrID;



            VM_rfdDetail refundDetail = new VM_rfdDetail()
            {
                refund = db.Refund.Where(r => r.rfdID == rfdID).ToList(),
                cart_orderDetail = db.Cart_OrderDetail.Where(c => c.odrID == odrID).ToList()
            };

            return View(refundDetail);


        }

        [HttpPost]
        public ActionResult cancel(string odrID)
        {


            var editods = db.Order.Where(m => m.odrID == odrID).FirstOrDefault();
            editods.odrStatusID = "ods0000008";
            var getspc = db.Cart_OrderDetail.Where(m => m.odrID == odrID).Select(m => m.spcID).ToList();
            db.SaveChanges();

            foreach (var item in getspc)

            {
                var spc = db.Specification.Where(m => m.spcID == item).FirstOrDefault();
                var quantity = db.Cart_OrderDetail.Where(m => m.odrID == odrID && m.spcID == item).FirstOrDefault().Quantity;
                var stock = spc.Stock;
                spc.Stock = quantity + stock;
                db.SaveChanges();

            }





            return RedirectToAction("OrderIndex");
        }

        public ActionResult rfd(string odrID, int code)
        {
            if (code == 1)
            {
                var rfd = db.Refund.Where(m => m.odrID == odrID).FirstOrDefault();
                rfd.rfdStatusID = "rds0000002";
            }
            if (code == 2)
            {
                var rfd = db.Refund.Where(m => m.odrID == odrID).FirstOrDefault();
                rfd.rfdStatusID = "rds0000004";
            }

            db.SaveChanges();
            return RedirectToAction("rfdIndex");
        }

        public ActionResult rfd2(string odrID)
        {

            var rfd = db.Refund.Where(m => m.odrID == odrID).FirstOrDefault();
            rfd.rfdStatusID = "rds0000003";


            db.SaveChanges();
            return RedirectToAction("rfdIndex");
        }

        public ActionResult editMarket()
        {
            string id = Session["memberID"].ToString();

            if (db.Seller.Where(m => m.mbrID == id).Count() == 0)
            {
                return RedirectToAction("SellerCreate", "SellerCertification");
            }


            string getselID = db.Seller.Where(m => m.mbrID == id).FirstOrDefault().selID;
            var market = db.Seller.Where(m => m.selID == getselID).FirstOrDefault();

            return View(market);

        }
        [HttpPost]
        public ActionResult editMarket(Seller seller)
        {
            string id = Session["memberID"].ToString();
            string getselID = db.Seller.Where(m => m.mbrID == id).FirstOrDefault().selID;
            var market = db.Seller.Where(m => m.selID == getselID).FirstOrDefault();

            market.selCompany = seller.selCompany;
            market.selInfo = seller.selInfo;
            db.SaveChanges();


            return RedirectToAction("OrderIndex");

        }


        public ActionResult productIndex()
        {
            string id = Session["memberID"].ToString();
            string getselID = db.Seller.Where(m => m.mbrID == id).FirstOrDefault().selID;

            var pdt = db.Product.Where(m => m.selID == getselID).ToList();

            return View(pdt);
        }
        public ActionResult QAIndex(int code = 0)

        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            string id = Session["memberID"].ToString();
            ViewBag.MemberName = db.Member.Where(m => m.mbrID == id).FirstOrDefault().nickName;
            IEnumerable<object> qa;
            string getselID = db.Seller.Where(m => m.mbrID == id).FirstOrDefault().selID;

            // 全部的問題
            if (code == 0)
            {
                qa = db.QA.Where(m => m.Product.selID == getselID).ToList().OrderByDescending(m => m.qaTime);

            }
            // 未回覆的問題
            else if (code == 1)
            {
                qa = db.QA.Where(m => m.Product.selID == getselID).Where(m => m.Answer == null || m.Answer == "").ToList().OrderByDescending(m => m.qaTime);

            }
            //已回覆的問題
            else if (code == 2)
            {
                qa = db.QA.Where(m => m.Product.selID == getselID).Where(m => m.Answer != null).Where(m => m.Answer != "").ToList().OrderByDescending(m => m.qaTime);

            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(qa);




        }

        public ActionResult myProduct(string fstID = null)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");
            string id = Session["memberID"].ToString();
            string getselID = db.Seller.Where(m => m.mbrID == id).FirstOrDefault().selID;
            ViewBag.fstLayer = db.Product.Where(m => m.selID == getselID).Select(m => m.ProductCategory.FirstLayer).Distinct().ToList();

            if (fstID == null)
            {
                var pdt = db.Product.Where(m => m.selID == getselID).ToList();
                return View(pdt);

            }

            var pdt2 = db.Product.Where(m => m.selID == getselID && m.ProductCategory.fstLayerID == fstID).ToList();


            return View(pdt2);

        }

        public PartialViewResult _GetProductCardFst(string productID, string fstID = null)
        {
            if (Session["memberID"] != null)
            {
                string id = Session["memberID"].ToString();
                ViewBag.id = id;
                if (db.Seller.Where(m => m.mbrID == id).Count() != 0)
                {
                    string getselID = db.Seller.Where(m => m.mbrID == id).FirstOrDefault().selID;

                    ViewBag.id = getselID;
                }
            }

            if (fstID == null)
            {
                var product1 = db.Product.Where(m => m.pdtID == productID).FirstOrDefault();
                return PartialView(product1);

            }

            var product = db.Product.Where(m => m.pdtID == productID && m.ProductCategory.fstLayerID == fstID).FirstOrDefault();
            return PartialView(product);
        }

        public ActionResult selEdit()
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");

            string id = Session["memberID"].ToString();
            if (db.Seller.Where(m => m.mbrID == id).Count() == 0)
            {
                return RedirectToAction("SellerCreate", "SellerCertification");
            }


            var seller = db.Seller.Where(m => m.mbrID == id).FirstOrDefault();
            return View(seller);


        }
        [HttpPost]
        public ActionResult selEdit(Seller m, HttpPostedFileBase Image)
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");


            string fileName = "";

            if (Image != null)
            {
                if (Image.ContentLength > 0)
                {

                    fileName = m.selID + ".jpg";
                    Image.SaveAs(Server.MapPath("~/sellerImage/" + fileName));
                }
            }
            else
            {
                fileName = m.selImage;
            }


            var seller = db.Seller.Where(a => a.mbrID == m.mbrID).FirstOrDefault();
            seller.selCompany = m.selCompany;
            seller.selCity = m.selCity;
            seller.selDist = m.selDist;
            seller.selAddress = m.selAddress;
            seller.selInfo = m.selInfo;
            seller.selImage = fileName;






            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("OrderIndex");
            }

            else
                return View(m);





        }


    }

}