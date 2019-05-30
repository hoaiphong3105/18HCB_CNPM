using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Controllers
{
    public class ChefController : Controller
    {
        // GET: Chef
        public ActionResult Index()
        {
            return View();
        }
    }
}