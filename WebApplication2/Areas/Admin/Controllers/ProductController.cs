using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Context;
using static WebApplication2.Common;

namespace WebApplication2.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        webbandtEntities objwebbandtEntities = new webbandtEntities();
        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product>();
            if (SearchString !=null)
            {
                page = 1;
            }    
            else
            {
                SearchString = currentFilter;
            }    
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = objwebbandtEntities.Products.Where(n  => n.Name.Contains(SearchString)).ToList();
            }  
            else
            {
                lstProduct = objwebbandtEntities.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ??  1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            
            return View();
        }

        [ValidateInput(false)]       
        
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            this.LoadData();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                        fileName = fileName + extension;
                        objProduct.Avartar = fileName;
                        objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                    }
                    objProduct.CreatedOnUtc = DateTime.Now;
                    objwebbandtEntities.Products.Add(objProduct);
                    objwebbandtEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(objProduct);

        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objwebbandtEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objwebbandtEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objwebbandtEntities.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objwebbandtEntities.Products.Remove(objProduct);
            objwebbandtEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objProduct = objwebbandtEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);    
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Product objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                if (objProduct.ImageUpLoad != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                    fileName = fileName + extension;
                    objProduct.Avartar = fileName;
                    objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                objwebbandtEntities.Entry(objProduct).State = EntityState.Modified;
                objwebbandtEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(objProduct);

        }

        void LoadData()
        {
            Common objCommon = new Common();
            var lstCategory = objwebbandtEntities.Categories.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCategory);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            var lstBrand = objwebbandtEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");


            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "SALE SẬP SÀN";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "ĐỀ XUẤT";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }


        [HttpGet]
        public ActionResult Create1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create1(Product objProduct)
        {
            return View();

        }

        
    }
}