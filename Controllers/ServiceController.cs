using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public HMSdbEntities objHMSdbEntities;
        public ServiceController()
        {
            objHMSdbEntities = new HMSdbEntities();
        }
        public ActionResult Index()
        {
            

            return View();
        }
    }
}