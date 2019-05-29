using Newtonsoft.Json;
using RestaurantMng.Service.Interfaces;
using RestaurantMng.WebApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection.Metadata;
using System.Web;
using System.Web.Mvc;
using System.Windows.Documents;
using static System.Collections.Specialized.BitVector32;

namespace RestaurantMng.WebApplication.Controllers
{
    public class CashierController : Controller
    {
        private readonly ICashierService _iOrderService;
        public CashierController(ICashierService iOrderService)
        {
            _iOrderService = iOrderService;
        }
        // GET: Manage

        public ActionResult Index()
        {
            var res = _iOrderService.GetAllOrder();
            var json = JsonConvert.SerializeObject(res);
            var resVM = JsonConvert.DeserializeObject<List<OrderOrderItemVM>>(json);



            return View(resVM);
        }
        [HttpGet]
        public ActionResult DetailOrder()
        {
            return PartialView("PVDetailOrder", null);
        }
        [HttpPost]
        public ActionResult DetailOrder(int? tableId, int? orderId)
        {
            if (orderId == null)
                return PartialView("PVDetailOrder", null);
            var res = _iOrderService.GetDetailOrder(orderId.Value);
            var json = JsonConvert.SerializeObject(res);
            var resVM = JsonConvert.DeserializeObject<List<OrderItemDetailVM>>(json);

            return Json(resVM, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult TinhTien(int orderId)
        {
            var res = _iOrderService.ThanhToan(orderId);
            if (!res)
                return new HttpStatusCodeResult(500, "Internal server error!");

            

            return Json(new { message = "thanh cong" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            return View();
        }
    }
}