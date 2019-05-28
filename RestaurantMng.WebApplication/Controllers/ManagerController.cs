using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menu()
        {
            return PartialView("PVTopGiaCao");
        }

        public ActionResult DetailOrder(int tableId, int orderId)
        {
            return Json(new { id = 23123, iddg = "khong co gi"}, JsonRequestBehavior.AllowGet);
        }
    }
}