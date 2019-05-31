using RestaurantMng.Core.Common;
using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.WebApplication.Authorization;
using RestaurantMng.WebApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Controllers
{
    [Authorization(Role = SystemRole.Quanly)]
    [RoutePrefix("quan-ly")]
    public class RestaurantStatusController : Controller
    {
        private readonly ITableService _iTableService;
        private readonly IOrderService _iOrderService;

        public RestaurantStatusController(IOrderService iOrderService, ITableService iTableService)
        {
            _iOrderService = iOrderService;
            _iTableService = iTableService;
        }
        // GET: RestaurantStatus
        public ActionResult Index()
        {
            var unpayOrder = _iOrderService.GetAll().Data
                .Where(x => x.PaymentStatus == 0).ToList();
            var countWaitingTable = 0;

            if (unpayOrder != null)
            {
                foreach (var order in unpayOrder)
                {
                    var orderItem = _iOrderService.GetOrderItems().Data
                        .Where(x => x.OrderId == order.OrderId && x.Status != 1).FirstOrDefault();
                    if (orderItem != null)
                    {
                        countWaitingTable++;
                    }
                }
            }
            ViewBag.WaitingTable = countWaitingTable;

            var tableList = _iTableService.GetAllTable().Data.Where(x => x.Status == 0).ToList();
            var tableInfoList = new List<TableInfoVM>();
            if(tableList != null && tableList.Count > 0)
            {
                foreach(var table in tableList)
                {
                    var orderItems = _iOrderService.GetOrderItems().Data
                        .Where(x => x.Order.TableId == table.TableId).ToList();
                    var orderInfoList = new List<OrderInfoVM>();
                    foreach(var orderItem in orderItems)
                    {
                        var menuItem = _iOrderService.GetAllMenu().Data
                            .Where(x => x.MenuItemId == orderItem.MeniItemId).FirstOrDefault();
                        var status = string.Empty;
                        if(orderItem.QuantityLate > 0)
                        {
                            status = "Bị trễ";
                        }
                        else if(orderItem.QuantityInProgress > 0)
                        {
                            status = "Đang chờ";
                        }
                        else
                        {
                            status = "Đã xong các món";
                        }
                        var orderInfo = new OrderInfoVM
                        {
                            Name = menuItem.Name,
                            Quantity = orderItem.Quantity,
                            Status = status,
                        };
                        orderInfoList.Add(orderInfo);
                    }
                    var tableInfo = new TableInfoVM
                    {
                        ID = table.TableId,
                        TableName = table.TableName,
                        OrderItems = orderInfoList,
                    };
                    tableInfoList.Add(tableInfo);
                }
            }
            return View(tableInfoList);
        }
    }
}