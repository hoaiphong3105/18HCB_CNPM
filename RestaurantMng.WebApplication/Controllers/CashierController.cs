using Newtonsoft.Json;
using RestaurantMng.Service.Interfaces;
using RestaurantMng.WebApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


            var resVM2 = res as IEnumerable<object>;

            var resVM3 = resVM2 as IEnumerable<OrderOrderItemVM>;



            return View(resVM2);
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

        [HttpGet]
        public ActionResult GetAll()
        {
            return View();
        }
    }
}