using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.ViewModels;
using DeWay.Models;


namespace DeWay.Controllers
{
    public class product_createController : Controller
    {
        // GET: product_create
        shopDBEntities db = new shopDBEntities();
        public ActionResult Create()
        {
            if (Session["memberID"] == null)
                return RedirectToAction("Login", "Login");



            ViewBag.fst = new SelectList(db.FirstLayer, "fstLayerID", "fstLayer");

            string id = Session["memberID"].ToString();

            if (db.Seller.Where(m => m.mbrID == id).Count() == 0)
            {
                return RedirectToAction("SellerCreate", "SellerCertification");
            }



            VM_pdtCreate vm = new VM_pdtCreate();
            vm.firstLayers = db.FirstLayer.ToList();
            ViewBag.shipperDetail = db.Shipper.ToList();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Create(VM_pdtCreate product, Specification[] specification, HttpPostedFileBase[] photo, ProductImage[] image, ShipperDetail[] shipperDetail, Tag[] tag, string fstID, string sndID, string trdID)
        {
            ViewBag.fst = new SelectList(db.FirstLayer, "fstLayerID", "fstLayer");
            ViewBag.shipperDetail = db.Shipper.ToList();
            if (photo[0] == null)
            {
                ModelState.AddModelError("Photo", "沒有封面圖片");
            }

            if (ModelState.IsValid != true)
            {
                return View();
            }

            
            string id = Session["memberID"].ToString();
            string getselID = db.Seller.Where(m => m.mbrID == id).FirstOrDefault().selID;

            var getctgID = db.ProductCategory.Where(m => m.fstLayerID == fstID && m.sndLayerID == sndID && m.trdLayerID == trdID).FirstOrDefault().pdtCtgID;




            string GetpdtID = db.Database.SqlQuery<string>("Select dbo.GetProductID()").FirstOrDefault();
            //傳入ProductVM(ViewModel)的products(ViewModel中的Product資料表)
            product.products.pdtID = GetpdtID;
            product.products.selID = getselID;  //用Session["member"]查
            product.products.pdtDate = DateTime.Now;
            product.products.Discontinued = false;
            product.products.ctgID = getctgID;  //擱置，資料表設計錯誤
            db.Product.Add(product.products);
            if (ModelState.IsValid == true)
            {

                db.SaveChanges();
            }

            //傳入規格資料表
            for (int i = 0; i < specification.Length; i++)
            {

                Specification spc = new Specification();
                spc.pdtID = GetpdtID;
                spc.Style = specification[i].Style;
                spc.Size = specification[i].Size;
                spc.Stock = specification[i].Stock;
                spc.Price = specification[i].Price;
                spc.Discount = 1;
                string GetspcID = db.Database.SqlQuery<string>("Select dbo.GetSpecificationID()").FirstOrDefault();
                spc.spcID = GetspcID;



                db.Specification.Add(spc);
                if (ModelState.IsValid == true)
                {

                    db.SaveChanges();
                }
            }

            //傳入照片


            string fileName = "";

            for (int i = 0; i < photo.Length; i++)
            {
                ProductImage img = new ProductImage();
                HttpPostedFileBase f = (HttpPostedFileBase)photo[i];
                if (f != null)
                {
                    if (f.ContentLength > 0)
                    {

                        string GetpImgID = db.Database.SqlQuery<string>("Select dbo.GetpImgID()").FirstOrDefault();

                        fileName = GetpImgID + ".jpg";
                        f.SaveAs(Server.MapPath("~/productImage/" + fileName));
                        img.pImgID = GetpImgID;
                        img.pdtID = GetpdtID;
                        img.pdtImage = GetpImgID + ".jpg";

                        db.ProductImage.Add(img);
                        if (ModelState.IsValid == true)
                        {

                            db.SaveChanges();
                        }


                    }

                }
            }
            //選擇運費
            for (int i = 0; i < shipperDetail.Length; i++)
            {
                ShipperDetail shp = new ShipperDetail();
                shp.shpID = shipperDetail[i].shpID;
                shp.pdtID = GetpdtID;
                shp.defaultShipping = shipperDetail[i].defaultShipping;
                db.ShipperDetail.Add(shp);
                if (ModelState.IsValid == true)
                {

                    db.SaveChanges();
                }
            }

            //寫入tag
                for (int i = 0; i < tag.Length; i++)
                {
                    Tag tg = new Tag();
                    string GetTagID = db.Database.SqlQuery<string>("Select dbo.GetTagID()").FirstOrDefault();
                    tg.tagID = GetTagID;
                    tg.tagName = tag[i].tagName;
                    tg.pdtID = GetpdtID;
                    db.Tag.Add(tg);
                    if (ModelState.IsValid == true)
                    {
                        db.SaveChanges();
                    }
                }



            //ViewBag.shp123 = new SelectList(db.Shipper, "shpID", "shpMethod");
            ViewBag.shipperDetail = db.Shipper.ToList();
            if (ModelState.IsValid == true)
            {
                return RedirectToAction("myProduct", "SellerHome");
            }

            return View(product);
        }

        [HttpPost]
        public JsonResult snd(string fstID)
        {
            List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrWhiteSpace(fstID))
            {
                var snd = this.Getsnd(fstID);
                if (snd.Count() > 0)
                {
                    foreach (var m in snd)
                    {
                        items.Add(new KeyValuePair<string, string>(m.sndLayerID, m.sndLayer));
                    }
                }
            }



            return this.Json(items);
        }

        private IEnumerable<SecondLayer> Getsnd(string fstID)
        {
            var getsndID = db.ProductCategory.Where(m => m.fstLayerID == fstID).Select(m => m.sndLayerID).Distinct().ToList();

            var sndList = db.SecondLayer.Where(m => getsndID.Contains(m.sndLayerID)).ToList();

            return sndList.ToList();

        }
        [HttpPost]
        public JsonResult trd(string sndID)
        {
            List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrWhiteSpace(sndID))
            {
                var trd = this.Gettrd(sndID);
                if (trd.Count() > 0)
                {
                    foreach (var m in trd)
                    {
                        items.Add(new KeyValuePair<string, string>(m.trdLayerID, m.trdLayer));
                    }
                }
            }



            return this.Json(items);
        }

        private IEnumerable<ThirdLayer> Gettrd(string sndID)
        {
            var gettrdID = db.ProductCategory.Where(m => m.sndLayerID == sndID).Select(m => m.trdLayerID).Distinct().ToList();

            var trdList = db.ThirdLayer.Where(m => gettrdID.Contains(m.trdLayerID)).ToList();

            return trdList.ToList();

        }
    }
}