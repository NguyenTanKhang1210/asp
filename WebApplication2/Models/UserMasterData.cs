using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Context;

namespace WebApplication2.Models
{
    public class UserMasterData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        private string password;
        public Nullable<bool> IsAdmin { get; set; }
        public Nullable<bool> Sex { get; set; }
    }
}