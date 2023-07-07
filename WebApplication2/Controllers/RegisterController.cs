using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Context;

namespace WebApplication2.Controllers
{
    public class RegisterController : Controller
    {
        webbandtEntities objwebbandtEntities = new webbandtEntities();
        // GET: Register
        [HttpGet]
        public ActionResult User()
        {
            return View();
        }

        [HttpGet]
        public ActionResult User1()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult User(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objwebbandtEntities.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objwebbandtEntities.Configuration.ValidateOnSaveEnabled = false;
                    objwebbandtEntities.Users.Add(_user);
                    objwebbandtEntities.SaveChanges();
                    return RedirectToAction("User");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}