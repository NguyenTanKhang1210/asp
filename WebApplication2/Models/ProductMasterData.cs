using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public partial class ProductMasterData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avartar { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string ShortDes { get; set; }
        public string FullDescription { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> PriceDiscount { get; set; }
        public Nullable<int> TypeId { get; set; }
        public string Slug { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }


    }
}