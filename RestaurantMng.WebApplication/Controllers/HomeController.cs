using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult MenuManager()
        {
            return PartialView("PVMenuManager");
        }
        public ActionResult MenuChef()
        {
            return PartialView("PVMenuChef");
        }
        public ActionResult MenuEmployee()
        {
            return PartialView("PVMenuEmployee");
        }
        public ActionResult MenuCashier()
        {
            return PartialView("PVMenuCashier");
        }
    }
}