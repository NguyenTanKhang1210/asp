using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Context;

namespace WebApplication2.Models
{
    public class Search
    {
        webbandtEntities objwebbandtEntities = new webbandtEntities();

        public List<Product> SearchByKey(string key)
        {
            return objwebbandtEntities.Products.SqlQuery("Select * From Product Where Name like '%" + key + "%'").ToList();
        }

    }
}