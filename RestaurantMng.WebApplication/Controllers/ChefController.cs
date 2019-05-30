using RestaurantMng.Service.User.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Controllers
{
    public class ChefController : Controller
    {
        private readonly IOrderService _iOrderService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iOrderService"></param>
        public ChefController(IOrderService iOrderService)
        {
            _iOrderService = iOrderService;
        }

        // GET: Chef
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOrderItems()
        {
            var orderItems = _iOrderService.GetOrderItems();
            var obj = new
            {
                Code = orderItems.Code,
                Message = orderItems.Message,
                Data = orderItems.Data.Select(x => new
                {
                    OrderItemId = x.OrderItemId,
                    MenuId =x.MeniItemId,
                    MenuName = x.Menu.Name,
                    Note= x.Note,
                    Quantity =x.Quantity,
                    Completed = x.QuantityCompleted,
                    InProgress = x.QuantityInProgress,
                    QuantityLate= x.QuantityLate,
                }).ToList(),
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Cập nhật trạng thái
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatus(int orderItemId, int menuId, int inprogress, int completed,int late)
        {
            var orderItems = _iOrderService.UpdateStatus(orderItemId, menuId,inprogress, completed, late);
            var obj = new
            {
                Code = orderItems.Code,
                Message = orderItems.Message,
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}