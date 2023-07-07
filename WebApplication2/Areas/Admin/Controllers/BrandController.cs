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
    public class BrandController : Controller
    {
        webbandtEntities objwebbandtEntities = new webbandtEntities();
        // GET: Admin/Brand
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstBrand = new List<Brand>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstBrand = objwebbandtEntities.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = objwebbandtEntities.Brands.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
          

            return View();
        }

        [ValidateInput(false)]

        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {
          

            if (ModelState.IsValid)
            {
                try
                {
                    if (objBrand.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpLoad.FileName);
                        string extension = Path.GetExtension(objBrand.ImageUpLoad.FileName);
                        fileName = fileName + extension;
                        objBrand.Avartar = fileName;
                        objBrand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                    }
                    objBrand.CreatedOnUtc = DateTime.Now;
                    objwebbandtEntities.Brands.Add(objBrand);
                    objwebbandtEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(objBrand);

        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objBrand = objwebbandtEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = objwebbandtEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand objBra)
        {
            var objBrand = objwebbandtEntities.Brands.Where(n => n.Id == objBra.Id).FirstOrDefault();
            objwebbandtEntities.Brands.Remove(objBrand);
            objwebbandtEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = objwebbandtEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Brand objBrand)
        {
            if (ModelState.IsValid)
            {
                if (objBrand.ImageUpLoad != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpLoad.FileName);
                    string extension = Path.GetExtension(objBrand.ImageUpLoad.FileName);
                    fileName = fileName + extension;
                    objBrand.Avartar = fileName;
                    objBrand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                objwebbandtEntities.Entry(objBrand).State = EntityState.Modified;
                objwebbandtEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(objBrand);

        }
    }
}