using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SparshWeb.Models
{
    public class AdminUser
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
    }
}