using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Context;

namespace WebApplication2.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}