using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Context;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PaymentController : Controller
    {
        webbandtEntities objwebbandtEntities = new webbandtEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"]==null)
            {
                return RedirectToAction("User", "Login");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objwebbandtEntities.Orders.Add(objOrder);
                objwebbandtEntities.SaveChanges();
                int intOrderId = objOrder.Id;

                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach (var item  in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.OrderId = intOrderId;
                    obj.Quantity = item.Quantity;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objwebbandtEntities.OrderDetails.AddRange(lstOrderDetail);
                objwebbandtEntities.SaveChanges();
            }

            return View();
        }
    }
}