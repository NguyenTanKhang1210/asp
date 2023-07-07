using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Context;

namespace WebApplication2.Controllers
{
    public class CategoryController : Controller
    {
        webbandtEntities objwebbandtEntities = new webbandtEntities();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objwebbandtEntities.Categories.ToList();
            return View(lstCategory);
        }
       
    }
}