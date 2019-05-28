using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Controllers
{
    public class CashierController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DetailOrder(int tableId, int orderId)
        {
            return Json(new { id = 23123, iddg = "khong co gi"}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult TinhTien(int tableId, int orderId)
        {
            return Json(new { message = "thanh cong", tongtien = "Tong tien" }, JsonRequestBehavior.AllowGet);
        }
    }
}