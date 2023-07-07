using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Context;

namespace WebApplication2.Controllers
{
    public class LargeController : Controller
    {
        webbandtEntities objwebbandtEntities = new webbandtEntities();
        // GET: Large
        public ActionResult Listing(int Id)
        {
            var listProduct = objwebbandtEntities.Products.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct);
        }
    }
}