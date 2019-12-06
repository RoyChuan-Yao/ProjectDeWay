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
            VM_pdtCreate vm = new VM_pdtCreate();
            vm.firstLayers = db.FirstLayer.ToList();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Create(VM_pdtCreate product, Specification[] specification, HttpPostedFileBase[] photo, ProductImage[] image)
        {
            string GetpdtID = db.Database.SqlQuery<string>("Select dbo.GetProductID()").FirstOrDefault();
            //傳入ProductVM(ViewModel)的products(ViewModel中的Product資料表)
            product.products.pdtID = GetpdtID;
            product.products.selID = "sel0000001";  //用Session["member"]查
            product.products.pdtDate = DateTime.Now;
            product.products.Discontinued = true;
            product.products.ctgID = "ctg0000001";  //擱置，資料表設計錯誤
            db.Product.Add(product.products);
            db.SaveChanges();


            //傳入規格資料表
            for (int i = 0; i < specification.Length; i++)
            {

                Specification spc = new Specification();
                spc.pdtID = GetpdtID;
                spc.Style = specification[i].Style;
                spc.Size = specification[i].Size;
                spc.Stock = specification[i].Stock;
                spc.Price = specification[i].Price;
                string GetspcID = db.Database.SqlQuery<string>("Select dbo.GetSpecificationID()").FirstOrDefault();
                spc.spcID = GetspcID;



                db.Specification.Add(spc);
                db.SaveChanges();
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
                        db.SaveChanges();


                    }

                }
            }




            return View();
        }
    }
}