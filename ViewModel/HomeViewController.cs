using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.ViewModel
{
    public class HomeViewModel
    {
        
        public string UserPasword { get; set; }
        
        public string UserName { get; set; }
        
        public string UserRule { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }

        
    }
}