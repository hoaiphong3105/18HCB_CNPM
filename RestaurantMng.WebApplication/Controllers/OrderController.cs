﻿using Microsoft.AspNet.SignalR;
using RestaurantMng.Core.Common;
using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.Service.User.Models.Request;
using RestaurantMng.WebApplication.Authorization;
using RestaurantMng.WebApplication.Models.ViewModels;
using RestaurantMng.WebApplication.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RestaurantMng.WebApplication.Controllers
{
    [Authorization(Role = SystemRole.Phucvu)]
    [RoutePrefix("goi-mon")]
    public class OrderController : Controller
    {
        private readonly IOrderService _iOrderService;
        private readonly ITableService _iTableService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iOrderService"></param>
        public OrderController(IOrderService iOrderService, ITableService iTableService)
        {
            _iOrderService = iOrderService;
            _iTableService = iTableService;
        }

        // GET: Order
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTables()
        {
            var tables = _iTableService.GetAllTable();
            var obj = new
            {
                Code = tables.Code,
                Message = tables.Message,
                Data = tables.Data.Select(x => new
                {
                    Id = x.TableId,
                    Name = x.TableName,
                }).ToList(),
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTable"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDetailInfoTable(int idTable)
        {
            var info = _iOrderService.GetDetailInfoTable(idTable);
            var obj = new
            {
                Code = info.Code,
                Message = info.Message,
                Data = info.Data.Select(x => new
                {
                    OrderItemId = x.OrderItemId,
                    MenuId = x.MeniItemId,
                    MenuName = x.Menu.Name,
                    Note = x.Note,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Status = x.Status == 1 ? "Đang xử lý" : x.Status == 2 ? "Hoàn thành" : "Trễ",
                }).ToList(),
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAllMenu()
        {
            var info = _iOrderService.GetAllMenu();
            var obj = new
            {
                Code = info.Code,
                Message = info.Message,
                Data = info.Data.Select(x => new
                {
                   Id=  x.MenuItemId,
                   Name =x.Name,
                   Price =x.Price,
                }).ToList(),
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddOrder(OrderReq model)
        {
            model.servantId = Helper.LoginUser().UserID;
            var info = _iOrderService.Order(model);

            var obj = new
            {
                Code = info.Code,
                Message =info.Message
            };
            if(info.Code == 1)
            {
                var lstNoti = new List<NotificationOrder>();
                lstNoti.AddRange(info.Data.Select(x => new NotificationOrder()
                {
                    OrderItemId = x.OrderItemId,
                    MenuId = x.MeniItemId,
                    MenuName = x.Menu.Name,
                    Note = x.Note,
                    Quantity = x.Quantity,
                    Status = x.Status,
                }).ToList());

                // real-time cập nhật danh sách món ăn cho đầu bếp
                var hub = GlobalHost.ConnectionManager.GetHubContext<RestaurantMngHub>();
                hub.Clients.All.Send3(lstNoti);
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}