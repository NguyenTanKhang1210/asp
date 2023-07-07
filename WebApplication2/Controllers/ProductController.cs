using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Context;

namespace WebApplication2.Controllers
{
    public class ProductController : Controller
    {
        webbandtEntities objwebbandtEntities = new webbandtEntities();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objwebbandtEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}