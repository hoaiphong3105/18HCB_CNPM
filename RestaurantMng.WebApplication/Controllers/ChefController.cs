using Microsoft.AspNet.SignalR;
using RestaurantMng.Core.Common;
using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.WebApplication.Authorization;
using RestaurantMng.WebApplication.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Controllers
{
    [Authorization(Role = SystemRole.Daubep)]
    [RoutePrefix("dau-bep")]
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
        [Route("danh-sach-mon-an")]
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
                    Status = x.Status,
                }).ToList(),
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Cập nhật trạng thái
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatus(int orderItemId, int status)
        {
            var orderItems = _iOrderService.UpdateStatus(orderItemId, status);
            var obj = new
            {
                Code = orderItems.Code,
                Message = orderItems.Message,
            };

            if (orderItems.Code == 1)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<RestaurantMngHub>();
                hub.Clients.All.Send2($"{orderItems.Data[0]}");
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}